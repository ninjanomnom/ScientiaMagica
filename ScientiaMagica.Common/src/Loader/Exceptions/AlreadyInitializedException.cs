namespace ScientiaMagica.Common.Loader.Exceptions {
    public class AlreadyInitializedException : PluginException {
        public AlreadyInitializedException() : base() { }
        public AlreadyInitializedException(string message) : base(message) { }
    }
}