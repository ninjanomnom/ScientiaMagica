using System;
using JetBrains.Annotations;
using ScientiaMagica.Common.Loader;

namespace ScientiaMagica.MainMenu {
    [UsedImplicitly]
    public class Main : InjectedPlugin<Main> {
        public override PluginIdentifier Info { get; } =
            new PluginIdentifier("Main Menu Controller", "ScientiaMagica.MainMenu.Core", new Version(0, 1, 0));
        
        
    }
}