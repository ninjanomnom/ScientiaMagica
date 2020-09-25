using System;
using System.Collections.Generic;
using ScientiaMagica.Common.Loader;

namespace GodotLibrary {
    public class Main : InjectedPlugin<Main> {
        // Required, this is for metadata relating to your plugin. Other mods use this to identify you.
        public override PluginIdentifier Info { get; }

        // Optional, plugin identifiers for other your plugin requires. The PluginIdentifier.Id and Version are how it gets matched.
        public override IEnumerable<PluginIdentifier> Dependencies { get; }

        // Optional, plugin identifiers for plugins your plugin is incompatible with.
        public override IEnumerable<PluginIdentifier> Incompatible { get; }

        // Optional, plugin identifiers for plugins yours should load after.
        public override IEnumerable<PluginIdentifier> LoadAfter { get; }

        // Optional, plugin identifiers for plugins yours should load before.
        public override IEnumerable<PluginIdentifier> LoadBefore { get; }

        // Optional, code to handle initialization if you need it. Most of your changes should be injected instead.
        public override void LoadPlugin(IDictionary<PluginIdentifier, IPlugin> plugins) { }
    }
}