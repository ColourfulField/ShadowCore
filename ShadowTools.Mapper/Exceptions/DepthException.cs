using System;

namespace ShadowTools.Mapper.Exceptions
{
    public class DepthException : Exception
    {
        public DepthException() {}

        public DepthException(string message) : base(message) { }
    }
}
