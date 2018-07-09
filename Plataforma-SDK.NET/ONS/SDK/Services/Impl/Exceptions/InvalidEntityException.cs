using System;

namespace ONS.SDK.Services.Impl.Exceptions
{
    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(string message): base(message) {

        }
    }
}