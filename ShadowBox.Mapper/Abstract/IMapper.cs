using System.Threading.Tasks;
using ShadowBox.AutomaticDI.Interfaces;

namespace ShadowBox.Mapper.Abstract
{
    public interface IMapper : ISingletonLifetime
    {
        Task<TTarget> Map<TSource, TTarget>(TSource source, TTarget target = null) where TTarget : class;
    }

}
