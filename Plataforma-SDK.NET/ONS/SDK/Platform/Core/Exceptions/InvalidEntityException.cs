using System;

namespace ONS.SDK.Platform.Core.Exceptions
{
    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(string message): base(message) {

        }
    }
}