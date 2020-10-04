using System;
using ScientiaMagica.Common.Loader;

namespace ScientiaMagica.PluginLoader {
    public class Main : InjectedPlugin {
        public override PluginIdentifier Info { get; } = new PluginIdentifier(
            "Plugin Loader", "ScientiaMagica.PluginLoader.Core", new Version(0, 1, 0)
        );
    }
}