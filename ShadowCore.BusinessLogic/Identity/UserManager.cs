using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ShadowCore.Models.EntityFramework.Domain;

namespace ShadowCore.BusinessLogic.Identity
{
    public class UserManager : AspNetUserManager<ApplicationUser>
    {
    }
}
