using System;

namespace ScientiaMagica.Common.Loader.Exceptions {
    public class IncompatiblePluginException : PluginException {
        public IncompatiblePluginException() : base() { }
        public IncompatiblePluginException(string message) : base(message) { }
        public IncompatiblePluginException(string message, Exception innerException) : base(message, innerException) { }
    }
}