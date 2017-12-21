using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShadowTools.Mapper.Abstract;

namespace ShadowTools.Mapper.Extensions
{
    public static class MapperExtensions
    {
        public static async Task<IEnumerable<TDestination>> Map<TSource, TDestination>(
            this IMapper mapper, IEnumerable<TSource> source) where TSource : class
                                                              where TDestination : class, new()
        {
            if (source == null)
            {
                return null;
            }

            TDestination destination = new TDestination();

            return await Task.WhenAll(source.Select(x => mapper.Map(x, destination)));
        }

        public static async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> items)
        {
            return (await items).ToList();
        }

        public static async Task<T[]> ToArrayAsync<T>(this Task<IEnumerable<T>> items)
        {
            return (await items).ToArray();
        }
    }
}
