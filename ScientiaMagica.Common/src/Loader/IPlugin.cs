using System.Collections.Generic;
using DryIoc;
using JetBrains.Annotations;

namespace ScientiaMagica.Common.Loader {
    public interface IPlugin {
        [NotNull]
        PluginIdentifier Info { get; }
        [CanBeNull]
        IEnumerable<PluginIdentifier> Dependencies { get; }
        [CanBeNull]
        IEnumerable<PluginIdentifier> Incompatible { get; }
        [CanBeNull]
        IEnumerable<PluginIdentifier> LoadAfter { get; }
        [CanBeNull]
        IEnumerable<PluginIdentifier> LoadBefore { get; }

        /// <summary>
        /// This is what handles the initial dependency injection, plugins shouldn't override this themselves
        /// </summary>
        /// <param name="container">The DI container</param>
        void Load(IContainer container);
        
        /// <summary>
        /// This initialization function gets run concurrently along side every other plugin<br/>
        /// Do only internal setup or bad things happen
        /// </summary>
        void Initialize();
        
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