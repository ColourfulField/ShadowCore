using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.DAL.EntityFramework.Abstract.Identity
{
    public interface IUserManager : IScopedLifetime
    {
        Task<IdentityResult> CreateAsync(User user, string password);
    }
}
