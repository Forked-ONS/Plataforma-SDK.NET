using System;

namespace ONS.SDK.App.Exceptions {
    public class BadConfigException : Exception {
        public BadConfigException (string message) : base (message) { }
    }
}