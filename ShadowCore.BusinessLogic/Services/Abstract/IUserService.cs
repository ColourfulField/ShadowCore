using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShadowCore.Models.DTO;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.BusinessLogic.Services.Abstract
{
    public interface IUserService : IScopedLifetime
    {
        Task<Guid> CreateUser(UserDTO userDto);
    }
}
