namespace ScientiaMagica.Common.Loader.Controllers {
    /// <summary>
    /// Should be used for systems that need to do init processing<br/>
    /// Every system using this gets called concurrently
    /// </summary>
    public interface IConcurrentWorldInitializer {
        InitFlags ConcurrentInitFlags { get; }
        void ConcurrentInit();
    }
}