using System.Collections.Generic;
using System.Threading.Tasks;
using ShadowBox.AutomaticDI.Interfaces;

namespace ShadowBox.Mapper.Abstract
{
    public interface IAutoMapService : IScopedLifetime
    {
        Task AutoMap<TSource, TDestination>(TSource source, TDestination destination = null, int? maxDepth = null,
            IEnumerable<string> exclude = null) where TDestination : class;
    }
}
