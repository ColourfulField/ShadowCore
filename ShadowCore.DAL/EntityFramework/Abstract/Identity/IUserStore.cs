using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.AutomaticDI.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ShadowCore.Models.EntityFramework;

namespace ShadowCore.DAL.EntityFramework.Abstract.Identity
{
    public interface IUserStore : IScopedLifetime
    {
    }
}
