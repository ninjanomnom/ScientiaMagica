using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using ScientiaMagica.Common.Loader;
using ScientiaMagica.Common.Loader.Exceptions;

namespace ScientiaMagica.PluginLoader {
    public class PluginManager : IPluginManager {
        private bool _loaded = false;

        private readonly Dictionary<PluginIdentifier, List<PluginIdentifier>> _trackedDependencies =
            new Dictionary<PluginIdentifier, List<PluginIdentifier>>();

        private readonly List<PluginIdentifier> _missingDependencies = new List<PluginIdentifier>();
        public IReadOnlyCollection<PluginIdentifier> MissingDependencies { get; }

        private readonly SortedSet<PluginFactory> _sortedPlugins =
            new SortedSet<PluginFactory>(new PluginOrderComparer());

        public PluginManager() {
            MissingDependencies = new ReadOnlyCollection<PluginIdentifier>(_missingDependencies);
        }
        
        public void AddPlugin(PluginFactory factory) {
            if (_loaded) {
                // TODO: Exception for already having loaded plugins
                throw new NotImplementedException();
            }

            if ((factory.Dependencies?.Any()).GetValueOrDefault(false)) {
                TrackDependencies(factory, factory.Dependencies);
            }

            _sortedPlugins.Add(factory);
        }

        private void TrackDependencies(PluginFactory dependent, IEnumerable<PluginIdentifier> dependencies) {
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

        public IEnumerable<IPlugin> LoadPlugins(IKernel kernel) {
            if (!_missingDependencies.Any()) {
                // TODO: Missing dependencies exception
                throw new NotImplementedException();
            }

            InitializePlugins(kernel);
            _loaded = true;
            return _sortedPlugins.Select(f => f.Get());
        }

        private void InitializePlugins(IKernel kernel) {
            void InitializePlugin(PluginFactory factory) {
                try {
                    factory.Initialize(kernel);
                }
                catch (Exception e) {
                    throw new PluginInitializationException($"{factory.Info} errored during initialization.", e);
                }
            }
            
            Parallel.ForEach(_sortedPlugins, InitializePlugin);
        }
    }
}