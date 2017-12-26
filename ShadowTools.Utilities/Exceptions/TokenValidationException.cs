using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowTools.Utilities.Exceptions
{
    public class TokenValidationException: ValidationException
    {
        public TokenValidationException(string message) : base(message)
        {
        }
    }
}
