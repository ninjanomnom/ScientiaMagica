using System;
using System.Collections.Generic;
using Ninject.Modules;
using ScientiaMagica.Common.Loader;

namespace ScientiaMagica.PluginLoader {
    public class Main : InjectedPlugin<Main> {
        public override PluginIdentifier Info { get; } = new PluginIdentifier(
            "Plugin Loader", "ScientiaMagica.PluginLoader.Core", new Version(0, 1, 0)
        );
    }
}