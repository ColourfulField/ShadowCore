using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShadowBox.Mapper.Abstract;

namespace ShadowBox.Mapper.Extensions
{
    public static class MapperExtensions
    {
        public static async Task<IEnumerable<TDestination>> Map<TSource, TDestination>(
            this IMapper mapper, IEnumerable<TSource> source, Func<TDestination> destination = null) where TDestination : class, new()
        {
            if (source == null)
                return Enumerable.Empty<TDestination>();

            if (destination == null) destination = () => new TDestination();
            return await Task.WhenAll(source.Select(x => mapper.Map(x, destination())));
        }

        public static async Task<IList<T>> ToListAsync<T>(this Task<IEnumerable<T>> tasks)
        {
            return (await tasks).ToList();
        }

        public static async Task<T[]> ToArrayAsync<T>(this Task<IEnumerable<T>> tasks)
        {
            return (await tasks).ToArray();
        }
    }
}
