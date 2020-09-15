using System;

namespace ScientiaMagica.Common.Loader.Exceptions {
    public class CircularReferencePluginException : PluginException {
        public CircularReferencePluginException() : base() { }
        public CircularReferencePluginException(string message) : base(message) { }
        public CircularReferencePluginException(string message, Exception innerException) : base(message, innerException) { }
    }
}