using System.Collections.Generic;
using Ninject.Modules;
using ScientiaMagica.Common.Loader;

namespace ScientiaMagica.PluginLoader {
    public class Main : NinjectModule {
        public override void Load() {
            Bind<IPluginManager>().To<PluginManager>();
        }
    }
}