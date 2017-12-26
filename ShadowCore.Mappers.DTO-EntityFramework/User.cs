using System.Threading.Tasks;
using ShadowCore.Models.DTO;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.Mapper.Abstract;

namespace ShadowCore.Mappers.DTO_EntityFramework
{
    public class UserDTO_UserEF : AsyncMapping<UserDTO, User>
    {
        protected override async Task MapFieldsAsync(UserDTO source, User destination)
        {
            destination.Id = source.Id;
            destination.Email = source.Email;
        }
    }

    public class UserEF_UserDTO : AsyncMapping<User, UserDTO>
    {
        protected override async Task MapFieldsAsync(User source, UserDTO destination)
        {
            destination.Id = source.Id;
            destination.Email = source.Email;
        }
    }
}
