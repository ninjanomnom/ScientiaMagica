using System;

namespace ScientiaMagica.Common.Loader.Exceptions {
    public class MissingPluginException : PluginException {
        public MissingPluginException() : base() { }
        public MissingPluginException(string message) : base(message) { }
        public MissingPluginException(string message, Exception innerException) : base(message, innerException) { }
    }
}