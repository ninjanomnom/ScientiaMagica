using System;
using ScientiaMagica.Common.Loader;

namespace ScientiaMagica.Setup {
    public class CoreDependencies : InjectedPlugin {
        public override PluginIdentifier Info { get; } =
            new PluginIdentifier("ScientiaMagica Core", "ScientiaMagica.Core.Core", new Version(0, 1, 0));
    }
}