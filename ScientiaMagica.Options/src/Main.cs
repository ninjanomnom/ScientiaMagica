using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ScientiaMagica.Common.Loader;

namespace ScientiaMagica.Options {
    [UsedImplicitly]
    public class Main : InjectedPlugin {
        public override PluginIdentifier Info { get; } =
            new PluginIdentifier("Main Menu Options", "ScientiaMagica.Options.Core", new Version(0, 1, 0));
    }
}