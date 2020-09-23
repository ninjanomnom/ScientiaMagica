namespace ScientiaMagica.Common.Loader.Exceptions {
    public class InvalidInjectionTypeException : PluginException {
        public InvalidInjectionTypeException() : base() { }
        public InvalidInjectionTypeException(string message) : base(message) { }
    }
}