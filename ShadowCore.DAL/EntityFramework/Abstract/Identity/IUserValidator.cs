using System.Threading.Tasks;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.DAL.EntityFramework.Abstract.Identity
{
    public interface IUserValidator: IScopedLifetime
    {
        Task<bool> ValidateAsync(User user);
    }
}
