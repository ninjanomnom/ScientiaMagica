using System.Collections;
using System.Collections.Generic;
using Ninject;

namespace ScientiaMagica.Common.Loader {
    public interface IPluginManager {
        public IReadOnlyCollection<PluginIdentifier> MissingDependencies { get; }
        public void AddPlugin(IPlugin plugin);
        public void LoadPlugins(IKernel kernel);
    }
}