using Godot;
using Ninject.Modules;
using NLog;
using ScientiaMagica.Common.Resources;

namespace ScientiaMagica.Setup {
    public class CoreDependencies : NinjectModule {
        public override void Load() {
            Bind<ILogger>().To<Logger>().InSingletonScope();
            BindMainMenuLoaders();
        }

        private void BindMainMenuLoaders() {
            var mainMenuNodeLoader = new NodeLoader(@"res://ScientiaMagica/src/Menus/MainMenu.tscn");
            Bind(typeof(NodeLoader))
                .ToConstant(mainMenuNodeLoader)
                .Named("MainMenu");

            var mainMenuScriptLoader = new ScriptLoader(null);
            Bind<ScriptLoader>()
                .ToConstant(mainMenuScriptLoader)
                .Named("MainMenu");
                
        }
    }
}