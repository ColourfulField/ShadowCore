using System;

namespace ShadowCore.Common.Models.Authentication
{
    public class AuthToken
    {
        public string Token { get; set; }
        public string Issuer { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
