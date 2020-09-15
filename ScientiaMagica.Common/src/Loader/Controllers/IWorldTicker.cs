using System.Linq;

namespace ScientiaMagica.Common.Loader.Controllers {
    /// <summary>
    /// This should be used for systems that need to tick exactly once each game tick
    /// </summary>
    public interface IWorldTicker {
        TickFlags TickFlags { get; }
        int TickPriority { get; }
        void Tick();
    }
}