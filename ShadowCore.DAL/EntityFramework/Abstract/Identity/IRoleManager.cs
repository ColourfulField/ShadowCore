using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.DAL.EntityFramework.Abstract.Identity
{
    public interface IRoleManager : IScopedLifetime
    {
       
    }
}
