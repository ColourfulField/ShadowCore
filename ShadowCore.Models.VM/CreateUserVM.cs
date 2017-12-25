using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShadowCore.Models.VM
{
    public class CreateUserVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
