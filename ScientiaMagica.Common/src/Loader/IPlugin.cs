using System.Collections.Generic;

namespace ScientiaMagica.Common.Loader {
    public interface IPlugin {
        PluginIdentifier Info { get; }

        /// <summary>
        /// Full integration setup for the plugin<br/>
        /// This gets run sequentially according to the dependency tree<br/>
        /// Order is not guaranteed if plugin is not in either <see cref="LoadAfter"/> or <see cref="LoadBefore"/>
        /// </summary>
        /// <param name="plugins">All other plugins that may or may not be loaded yet.
        /// Only plugins in <see cref="LoadAfter"/> are guaranteed to be loaded
        /// and only plugins in <see cref="LoadBefore"/> are guaranteed to not be loaded.</param>
        void LoadPlugin(IDictionary<PluginIdentifier, IPlugin> plugins);
    }
}