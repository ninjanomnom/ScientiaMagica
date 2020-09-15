using Ninject.Modules;
using NLog;

namespace ScientiaMagica.Setup {
    public class CoreDependencies : NinjectModule {
        public override void Load() {
            Bind<ILogger>().To<Logger>().InSingletonScope();
        }
    }
}