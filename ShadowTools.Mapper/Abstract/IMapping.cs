using System.Threading.Tasks;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowTools.Mapper.Abstract
{

    public interface IMapping : ISingletonLifetime
    {
        Task<object> Map(object source, object destination);
    }

    public interface IMapping<TSource, TDestination> : IMapping where TDestination : class
    {
        Task<TDestination> Map(TSource source, TDestination destination);

    }
}
