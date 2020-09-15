using System;

namespace ScientiaMagica.Common.Loader.Exceptions {
    public class PluginInitializationException : PluginException {
        public PluginInitializationException() : base() { }
        public PluginInitializationException(string message) : base(message) { }
        public PluginInitializationException(string message, Exception innerException) : base(message, innerException) { }
    }
}