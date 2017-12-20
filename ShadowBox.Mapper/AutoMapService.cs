using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ShadowBox.Mapper.Abstract;
using ShadowBox.Mapper.Exceptions;
using ShadowBox.Utilities.Extensions.Reflection;

namespace ShadowBox.Mapper
{
    public class AutoMapService : IAutoMapService
    {
        private readonly Func<IMapper> _mapper; // Prevents Circular dependency

        public AutoMapService(Func<IMapper> mapper)
        {
            _mapper = mapper;
        }
        public static int DefaultMaxDepth { get; set; } = 3;

        public async Task AutoMap<TSource, TDestination>(TSource source, TDestination destination = null, int? maxDepth = null,
            IEnumerable<string> exclude = null) where TDestination : class
        {
            maxDepth = maxDepth ?? DefaultMaxDepth;
            exclude = exclude ?? new List<string>();
            await MapObjectFields(source, destination, 0, maxDepth.Value, exclude);
        }

        private async Task MapObjectFields<TSource, TDestination>(TSource source, TDestination destination, int currentDepth, int maxDepth,
            IEnumerable<string> exclude) where TDestination : class
        {
            if (currentDepth > maxDepth)
            {
                throw new DepthException("Current mapping depth exceeds max depth");
            }
            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
                if (destinationProperty == null || exclude.Any(x => x == destinationProperty.Name))
                {
                    continue;
                }

                //Simple Types
                var isMappingSuccesfull = TryMapSimpleObjects(source, destination, sourceProperty, destinationProperty);
                if (isMappingSuccesfull)
                {
                    continue;
                }

                //Complex Types
                await TryMapComplexObjects(source, destination, sourceProperty, destinationProperty, maxDepth);
            }
        }

        private bool TryMapSimpleObjects<TSource, TDestination>(
            TSource source,
            TDestination destination,
            PropertyInfo sourceProperty,
            PropertyInfo destinationProperty)
        {
            var sourcePropertyType = sourceProperty.PropertyType;
            var destinationPropertyType = destinationProperty.PropertyType;

            if (destinationPropertyType.IsSimple() && sourcePropertyType.IsEquivalentTo(destinationPropertyType))
            {
                destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                return true;
            }
            return false;

        }

        private async Task<bool> TryMapComplexObjects<TSource, TDestination>(
            TSource source,
            TDestination destination,
            PropertyInfo sourceProperty,
            PropertyInfo destinationProperty,
            int maxDepth)
        {
            var sourcePropertyType = sourceProperty.PropertyType;
            var destinationPropertyType = destinationProperty.PropertyType;

            if (CheckDepth(source, 0, maxDepth) > maxDepth)
            {
                source = source.Clone(maxDepth);
            }

            //TODO use depth to avoid cyclomatic dependencies. Use pre-map checker, which will cut out all dependencies, which are way too deep
            //TODO THERE IS A NEED TO CREATE CLONED SOURCE object
            //TODO lists?
            //await RemoveCyclomaticDependencies();
            //await ApplyDepthFilter();
            if (!sourcePropertyType.IsSimple() && !destinationPropertyType.IsSimple() && sourceProperty.GetValue(source) != null)
            {
                // if (currentDepth <= maxDepth)
                {
                    var mapper = _mapper();
                    var mapMethod = mapper.GetType().GetMethod("Map");
                    var genericMethod = mapMethod.MakeGenericMethod(sourcePropertyType, destinationPropertyType);
                    var sourceObject = Convert.ChangeType(sourceProperty.GetValue(source), sourcePropertyType);
                    var destinationObject = Activator.CreateInstance(destinationPropertyType);
                    await (Task)genericMethod.Invoke(mapper, new[] { sourceObject, destinationObject });

                    //await

                    destinationProperty.SetValue(destination, destinationObject);
                }
                //  else
                //{
                //    destinationProperty.SetValue(destination, null);
                //}
            }
            return true;
        }

        private int CheckDepth(object item, int depth, int maxDepth)
        {
            if (depth > maxDepth)
            {
                return depth;
            }
            
            var depths = new List<int> { depth };

            var itemProperties = item.GetType().GetProperties();
            foreach (var itemProperty in itemProperties)
            {
                if (!itemProperty.PropertyType.IsSimple() && itemProperty.GetValue(item) != null)
                {
                    depths.Add(CheckDepth(itemProperty.GetValue(item), depth + 1, maxDepth));
                }
            }

            return depths.Max();
        }
    }
}
