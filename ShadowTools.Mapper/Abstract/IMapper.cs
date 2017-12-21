using System.Threading.Tasks;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowTools.Mapper.Abstract
{
    public interface IMapper : ISingletonLifetime
    {
        Task<TTarget> Map<TSource, TTarget>(TSource source, TTarget target = null) where TTarget : class;
    }

}
