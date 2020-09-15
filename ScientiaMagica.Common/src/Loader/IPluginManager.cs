using System.Collections;
using System.Collections.Generic;
using Ninject;

namespace ScientiaMagica.Common.Loader {
    public interface IPluginManager {
        public IReadOnlyCollection<PluginIdentifier> MissingDependencies { get; }
        public void AddPlugin(PluginFactory factory);
        public IEnumerable<IPlugin> LoadPlugins(IKernel kernel);
    }
}