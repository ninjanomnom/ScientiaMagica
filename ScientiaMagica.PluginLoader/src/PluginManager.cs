using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using ScientiaMagica.Common.Loader;
using ScientiaMagica.Common.Loader.Attributes;
using ScientiaMagica.Common.Loader.Exceptions;

namespace ScientiaMagica.PluginLoader {
    [DefaultInject(typeof(IPluginManager))]
    public class PluginManager : IPluginManager {
        private bool _loaded;

        private readonly Dictionary<PluginIdentifier, List<PluginIdentifier>> _trackedDependencies =
            new Dictionary<PluginIdentifier, List<PluginIdentifier>>();

        private readonly List<PluginIdentifier> _missingDependencies = new List<PluginIdentifier>();
        public IReadOnlyCollection<PluginIdentifier> MissingDependencies { get; }

        private readonly SortedSet<IPlugin> _sortedPlugins =
            new SortedSet<IPlugin>(new PluginOrderComparer());

        public PluginManager() {
            MissingDependencies = new ReadOnlyCollection<PluginIdentifier>(_missingDependencies);
        }
        
        public void AddPlugin(IPlugin plugin) {
            if (_loaded) {
                throw new AlreadyInitializedException("The plugin manager is already initialized, plugins can no longer be added");
            }

            if ((plugin.Dependencies?.Any()).GetValueOrDefault(false)) {
                TrackDependencies(plugin, plugin.Dependencies);
            }

            _sortedPlugins.Add(plugin);
        }

        private void TrackDependencies(IPlugin dependent, IEnumerable<PluginIdentifier> dependencies) {
            _missingDependencies.Remove(dependent.Info);
            
            foreach (var dependency in dependencies) {
                if (_sortedPlugins.All(d => d.Info != dependency)) {
                    _missingDependencies.Add(dependency);
                }
                
                if (_trackedDependencies.TryGetValue(dependency, out var existingRegistry)) {
                    existingRegistry.Add(dependent.Info);
                }
                else {
                    _trackedDependencies.Add(dependency, new List<PluginIdentifier>(){dependent.Info} );
                }
            }
        }

        public void LoadPlugins() {
            if (_missingDependencies.Any()) {
                throw new AggregateException(_missingDependencies.Select(p =>
                    new MissingPluginException($"One or more plugins require {p} to be installed")));

            }

            InitializePlugins();
            _loaded = true;
            var initializedPlugins = _sortedPlugins.ToDictionary(p => p.Info, p => p);
            foreach (var plugin in _sortedPlugins) {
                plugin.LoadPlugin(initializedPlugins);
            }
        }

        private void InitializePlugins() {
            void InitializePlugin(IPlugin plugin) {
                try {
                    plugin.Initialize();
                }
                catch (Exception e) {
                    throw new PluginInitializationException($"{plugin.Info} errored during initialization.", e);
                }
            }
            
            Parallel.ForEach(_sortedPlugins, InitializePlugin);
        }
    }
}