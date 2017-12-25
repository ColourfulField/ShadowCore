using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.Models.EntityFramework.Domain;

namespace ShadowCore.DAL.EntityFramework.Identity
{
    public class ApplicationUserValidator : UserValidator<User>, IUserValidator
    {
    }
}
