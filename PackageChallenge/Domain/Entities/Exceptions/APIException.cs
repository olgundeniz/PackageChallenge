using System;

namespace Domain
{
    public class APIException : Exception
    {
        public APIException(string message) : base(message)
        {
        }
    }
}
