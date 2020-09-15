using System;

namespace ScientiaMagica.Common.Loader.Exceptions {
    public class PluginException : Exception {
        public PluginException() : base() { }
        public PluginException(string message) : base(message) { }
        public PluginException(string message, Exception innerException) : base(message, innerException) { }
    }
}