using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DryIoc;
using Godot;
using NLog;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Loader;
using ScientiaMagica.Common.Loader.Controllers;
using ScientiaMagica.Menus;
using Container = DryIoc.Container;
using Directory = System.IO.Directory;
using Path = System.IO.Path;
using World = ScientiaMagica.Common.World;

namespace ScientiaMagica.Setup {
    public class Setup {
        private readonly IContainer _container;
        private readonly ILogger _logger;

        public Setup() {
            _container = new Container(
                rules =>
                    rules.WithAutoConcreteTypeResolution()
                        .WithFuncAndLazyWithoutRegistration()
                        .WithTrackingDisposableTransients()
            );
            _container.RegisterInstance(this);
            
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void LoadPlugins() {
            LoadDependencies();
            var pluginManager = _container.Resolve<IPluginManager>();
            foreach (var plugin in _container.ResolveMany<IPlugin>()) {
                pluginManager.AddPlugin(plugin);
            }
            pluginManager.LoadPlugins();
        }
        
        private void LoadDependencies() {
            var directory = new Uri(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)
                ?? throw new InvalidOperationException("Assembly somehow not in a directory")
            ).AbsolutePath;
            
            var dllFiles = new string[] { }
                .Concat(Directory.GetFiles(directory, @"ScientiaMagica*.dll"))
                .Concat(Directory.GetFiles(directory, @"*.Main.dll"))
                .Select(s => new Uri(s).AbsolutePath)
                .Select(Assembly.LoadFile);

            foreach (var assembly in dllFiles)
            foreach (var def in assembly.GetTypes()) {
                if (!def.ImplementsServiceType<IPlugin>()) {
                    continue;
                };
                
                var instance = Activator.CreateInstance(def) as IPlugin;
                instance?.Load(_container);
            }
        }

        private bool IsRegisterEntrant(Type someType) {
            var result = someType.ImplementsServiceType<IPlugin>();
            return result;
        }

        public void InitializeGame() {
            var worldInitializers = 
                _container.ResolveMany<IWorldInitializer>().OrderBy(i => i.InitPriority);
            foreach (var i in worldInitializers) {
                HandleWorldInitializer(i);
            }

            var worldConcurrentInitializers = _container.ResolveMany<IConcurrentWorldInitializer>();
            Parallel.ForEach(worldConcurrentInitializers, HandleConcurrentWorldInitializer);

            var worldTickers = _container.ResolveMany<IWorldTicker>();
            var concurrentWorldTickers = _container.ResolveMany<IConcurrentWorldTicker>();
        }

        private void HandleWorldInitializer(IWorldInitializer worldInitializer) {
            var before = DateTime.Now;
            _logger.Info($"Initializing {worldInitializer}...");
            worldInitializer.Init();
            _logger.Info($"{worldInitializer} initialized in {(DateTime.Now - before).Seconds} seconds");
        }

        private void HandleConcurrentWorldInitializer(IConcurrentWorldInitializer worldInitializer) {
            var before = DateTime.Now;
            _logger.Info($"Initializing {worldInitializer}...");
            worldInitializer.ConcurrentInit();
            _logger.Info($"{worldInitializer} initialized in {(DateTime.Now - before).Seconds} seconds");
        }
        
        public void LoadMenu() {
            var buttons = _container.Resolve<IEnumerable<IMainMenuButton>>();
            var test = new CanvasLayer();

            var mainMenu = new MainMenu(buttons);
            
            //var mainMenu = _container.Resolve<MainMenu>();
            World.SwitchScene(mainMenu);
        }
    }
}