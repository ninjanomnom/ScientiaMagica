using System;

namespace ScientiaMagica.Common.Loader.Controllers {
    [Flags]
    public enum TickFlags {
        /// <summary>
        /// If just this flag is used, the ticker will never tick
        /// </summary>
        TickNever = 0,
        /// <summary>
        /// Causes the ticker to run even while the game is paused
        /// </summary>
        TickWhilePaused = 1 << 0,
        /// <summary>
        /// Causes the ticker to run even while the game is in a load screen
        /// </summary>
        TickInLoading = 1 << 1
    }
}