using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShadowCore.API.Controllers.Abstract;
using ShadowCore.BusinessLogic.Services.Abstract;
using ShadowCore.Models.DTO;
using ShadowCore.Models.VM.User;
using ShadowTools.Mapper.Abstract;

namespace ShadowCore.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper) : base(mapper: mapper)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserVM user)
        {
            var userId = await _userService.CreateUser(await Mapper.Map<CreateUserVM, UserDTO>(user));
            return FormattedResponse(userId);
        }

        
    }
}
