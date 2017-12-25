using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowCore.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
