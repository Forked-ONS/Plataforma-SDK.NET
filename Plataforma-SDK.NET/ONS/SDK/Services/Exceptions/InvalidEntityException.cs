using System;

namespace ONS.SDK.Services.Exceptions
{
    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(string message): base(message) {

        }
    }
}