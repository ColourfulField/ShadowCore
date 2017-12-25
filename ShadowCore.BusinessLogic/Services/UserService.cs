using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShadowCore.BusinessLogic.Services.Abstract;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.Models.DTO;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.Mapper.Abstract;

namespace ShadowCore.BusinessLogic.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserManager _userManager;

        public UserService(IMapper mapper, IUserManager userManager): base(mapper: mapper)
        {
            _userManager = userManager;
        }

        public async Task<Guid> CreateUser(UserDTO userDto)
        {
            var user = await Mapper.Map<UserDTO, User>(userDto);
            user.Id = Guid.NewGuid();
            await _userManager.CreateAsync(user, userDto.Password);

            return user.Id;
        }
    }
}
