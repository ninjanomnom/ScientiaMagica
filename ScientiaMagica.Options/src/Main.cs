using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ScientiaMagica.Common.Loader;

namespace ScientiaMagica.Options {
    [UsedImplicitly]
    public class Main : InjectedPlugin<Main> {
        public override PluginIdentifier Info { get; }

        public Main() {
            Info = new PluginIdentifier("Main Menu Options", "ScientiaMagica.Options.Core", new Version(0, 1, 0));
        }
        
        public override void LoadPlugin(IDictionary<PluginIdentifier, IPlugin> plugins) {
            //throw new System.NotImplementedException();
        }
    }
}