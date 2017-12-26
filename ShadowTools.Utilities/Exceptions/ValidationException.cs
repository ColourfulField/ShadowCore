using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

namespace ShadowTools.Utilities.Exceptions
{
    public class ValidationException: Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(Dictionary<string, string> validationErrors) : base(JsonConvert.SerializeObject(validationErrors))
        {
        }

        public ValidationException(IEnumerable<string> validationErrors) : base(JsonConvert.SerializeObject(validationErrors))
        {
        }
    }
}
