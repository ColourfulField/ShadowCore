using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ShadowTools.Mapper.Abstract;
using ShadowTools.Mapper.Exceptions;
using ShadowTools.Mapper.Options;
using ShadowTools.Utilities.Extensions.Reflection;

namespace ShadowTools.Mapper
{
    public class AutoMapService : IAutoMapService
    {
        private readonly Func<IMapper> _mapper; // Prevents Circular dependency

        public AutoMapService(Func<IMapper> mapper, IOptions<AutomapperOptions> automapperOptions)
        {
            _mapper = mapper;
            _defaultMaxDepth = automapperOptions?.Value.MaxDepth ?? 0;
        }

        private readonly int _defaultMaxDepth;

        public async Task AutoMap<TSource, TDestination>(TSource source, TDestination destination = null, int? maxDepth = null,
            IEnumerable<string> exclude = null) where TDestination : class
        {
            maxDepth = maxDepth ?? _defaultMaxDepth;
            exclude = exclude ?? new List<string>();
            await MapObjectFields(source, destination, 0, maxDepth.Value, exclude);
        }

        private async Task MapObjectFields<TSource, TDestination>(TSource source, TDestination destination, int currentDepth, int maxDepth,
            IEnumerable<string> exclude) where TDestination : class
        {
            if (currentDepth > maxDepth)
            {
                throw new DepthException($"Current mapping depth <{currentDepth}> exceeds max depth <{maxDepth}>");
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

                var isMappingSuccesfull = TryMapSimpleObjects(source, destination, sourceProperty, destinationProperty);
                if (isMappingSuccesfull)
                {
                    continue;
                }

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

            //TODO lists?
            if (!sourcePropertyType.IsSimple() && !destinationPropertyType.IsSimple() && sourceProperty.GetValue(source) != null)
            {

                var mapper = _mapper();
                var mapMethod = mapper.GetType().GetMethod("Map");
                var genericMethod = mapMethod.MakeGenericMethod(sourcePropertyType, destinationPropertyType);
                var sourceObject = Convert.ChangeType(sourceProperty.GetValue(source), sourcePropertyType);
                var destinationObject = Activator.CreateInstance(destinationPropertyType);
                await (Task) genericMethod.Invoke(mapper, new[] {sourceObject, destinationObject});

                destinationProperty.SetValue(destination, destinationObject);
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
