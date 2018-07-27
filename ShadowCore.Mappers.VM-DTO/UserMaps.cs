using System.Threading.Tasks;
using ShadowCore.Models.DTO;
using ShadowCore.Models.VM.User;
using ShadowTools.Mapper.Abstract;

namespace ShadowCore.Mappers.VM_DTO
{
    public class CreateUserVM_UserDTO : AsyncMapping<CreateUserVM, UserDTO>
    {
        protected override async Task MapFieldsAsync(CreateUserVM source, UserDTO destination)
        {
            destination.Email = source.Email;
            destination.Password = source.Password;
        }
    }

    public class UserLoginVM_UserDTO : AsyncMapping<UserLoginVM, UserDTO>
    {
        protected override async Task MapFieldsAsync(UserLoginVM source, UserDTO destination)
        {
            destination.Email = source.Email;
            destination.Password = source.Password;
        }
    }
}
