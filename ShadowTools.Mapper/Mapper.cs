using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShadowTools.Mapper.Abstract;

namespace ShadowTools.Mapper
{
    public class Mapper : IMapper
    {
        private readonly IEnumerable<IMapping> _mappings;

        //TODO test how fast is this resolver
        public Mapper(IEnumerable<IMapping> mappings)
        {
            _mappings = mappings;
        }

        //TODO cache already found mappings to improve performance
        public async Task<TDestination> Map<TSource, TDestination>(TSource source, TDestination destination = null) where TDestination : class
        {
            var genericType = typeof(IMapping<,>).MakeGenericType(typeof(TSource), typeof(TDestination));
            var mappingQuery =
                from m in _mappings
                let type = m.GetType()
                where genericType.IsAssignableFrom(type)
                select m;

            var matchingMappings = mappingQuery.ToList();

            if (!matchingMappings.Any())
                throw new InvalidProgramException($"No mapping found for {typeof(TSource).Name} and {typeof(TDestination).Name}.");

            if (matchingMappings.Count > 1)
                throw new InvalidProgramException($"Multiple mappings found for {typeof(TSource).Name} and {typeof(TDestination).Name}. Only one type should implement this mapping.");

            var foundMapping = matchingMappings.Single();
            return (TDestination)(await foundMapping.Map(source, destination));
        }
    }



    //public interface IMapping<TSource, TTarget> : ISingletonLifetime where TTarget : class
    //{
    //    Task<TTarget> Map(TSource source, TTarget target = null);
    //}

    //public abstract class AsyncMapping<TSource, TTarget> : IMapping<TSource, TTarget> where TTarget : class, new()
    //{
    //    async Task<TTarget> IMapping<TSource, TTarget>.Map(TSource source, TTarget target)
    //    {
    //        if (target == null)
    //        {
    //            target = new TTarget();
    //        }
    //        if (source != null)
    //        {

    //            await MapFieldsAsync(source, target);
    //        }
    //        return target;
    //    }

    //    protected abstract Task MapFieldsAsync(TSource source, TTarget target);

    //    async Task<object> IMapping.Map(object source, object target)
    //    {
    //        return await ((IMapping<TSource, TTarget>)this).Map((TSource)source, (TTarget)target);
    //    }
    //}

    //public abstract class Mapping<TSource, TTarget> : AsyncMapping<TSource, TTarget> where TTarget : class, new()
    //{
    //    protected override Task MapFieldsAsync(TSource source, TTarget target)
    //    {
    //        MapFields(source, target);
    //        return Task.CompletedTask;
    //    }

    //    protected abstract void MapFields(TSource source, TTarget target);
    //}
}
