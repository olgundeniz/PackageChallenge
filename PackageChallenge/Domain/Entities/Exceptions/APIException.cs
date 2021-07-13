using System;

namespace Domain
{
    public class APIException : Exception
    {
        public APIException() : base()
        {

        }
        public APIException(string message) : base(message)
        {
        }
    }
}
