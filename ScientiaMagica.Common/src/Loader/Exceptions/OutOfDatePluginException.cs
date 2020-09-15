using System;

namespace ScientiaMagica.Common.Loader.Exceptions {
    public class OutOfDatePluginException : PluginException {
        public OutOfDatePluginException() : base() { }
        public OutOfDatePluginException(string message) : base(message) { }
        public OutOfDatePluginException(string message, Exception innerException) : base(message, innerException) { }
    }
}