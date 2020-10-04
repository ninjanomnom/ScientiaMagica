using System;
using ScientiaMagica.Common.Loader;

namespace ScientiaMagica.Game {
    public class Main : InjectedPlugin {
        public override PluginIdentifier Info { get; } =
            new PluginIdentifier("Main Game", "ScientiaMagica.Game.Core", new Version(0, 1, 0));
    }
}