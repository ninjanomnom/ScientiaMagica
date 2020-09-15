namespace ScientiaMagica.Common.Loader.Controllers {
    /// <summary>
    /// This should be used for systems that need to tick exactly once each game tick<br/>
    /// Every system using this gets called concurrently
    /// </summary>
    public interface IConcurrentWorldTicker {
        TickFlags ConcurrentTickFlags { get; }
        void ConcurrentTick();
    }
}