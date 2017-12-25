using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShadowCore.Models.DTO;
using ShadowCore.Models.VM;
using ShadowTools.Mapper.Abstract;

namespace ShadowCore.Mappers.VM_DTO
{
    public class CreateUserMappingVMtoDTO : AsyncMapping<CreateUserVM, UserDTO>
    {
        protected override async Task MapFieldsAsync(CreateUserVM source, UserDTO destination)
        {
            destination.Email = source.Email;
            destination.Password = source.Password;
        }
    }
}
