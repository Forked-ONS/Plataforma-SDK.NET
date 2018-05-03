using System;

namespace ONS.PlataformaSDK.Exception
{
    public class PlataformaException : System.Exception
    {
        public PlataformaException() { }
        public PlataformaException(string message) : base(message) { }
        public PlataformaException(string message, System.Exception inner) : base(message, inner) { }
        protected PlataformaException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}