using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShadowCore.DAL.EntityFramework.Identity;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.DAL.EntityFramework.Abstract.Identity
{
    public interface IUserValidator: IScopedLifetime
    {
        Task<bool> ValidateAsync(User user);
    }
}
