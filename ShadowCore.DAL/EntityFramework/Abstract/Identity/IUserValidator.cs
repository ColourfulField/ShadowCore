using Microsoft.AspNetCore.Identity;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.DAL.EntityFramework.Abstract.Identity
{
    public interface IUserValidator: IScopedLifetime
    {
    }
}
