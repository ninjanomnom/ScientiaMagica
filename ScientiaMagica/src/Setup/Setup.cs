using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Godot;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Parameters;
using NLog;
using ScientiaMagica.Common.Loader;
using ScientiaMagica.Common.Loader.Controllers;
using ScientiaMagica.Common.Loader.Exceptions;
using ScientiaMagica.Menus;
using Directory = System.IO.Directory;
using File = Godot.File;
using Path = System.IO.Path;
using World = ScientiaMagica.Common.World;

namespace ScientiaMagica.Setup {
    public class Setup {
        private static readonly Regex CorePlugins = new Regex(@"ScientiaMagica\..*\.dll");
        private static readonly Regex ExternalPlugins = new Regex(@"Plugins");
        
        private readonly StandardKernel _kernel;
        private readonly ILogger _logger;

        public Setup() {
            _kernel = new StandardKernel(new CoreDependencies());
            _kernel.Bind<Setup>().ToSelf();
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void LoadPlugins() {
            LoadDependencies();
            var pluginManager = _kernel.Get<IPluginManager>();
            foreach (var plugin in _kernel.GetAll<IPlugin>()) {
                pluginManager.AddPlugin(plugin);
            }
            pluginManager.LoadPlugins(_kernel);
        }
        
        private void LoadDependencies() {

            var directory = new Uri(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)
                ?? throw new InvalidOperationException("Assembly somehow not in a directory")
            )?.AbsolutePath;
            
            var dllFiles = new string[] { }
                .Concat(Directory.GetFiles(directory, @"ScientiaMagica.*.dll"))
                .Concat(Directory.GetFiles(directory, @"*.Main.dll"))
                .Select(s => new Uri(s).AbsolutePath);

            _kernel.Load(dllFiles.Select(Assembly.LoadFile));
        }

        public void InitializeGame() {
            var worldInitializers = _kernel.GetAll<IWorldInitializer>().OrderBy(i => i.InitPriority);
            foreach (var i in worldInitializers) {
                HandleWorldInitializer(i);
            }
            
            var worldConcurrentInitializers = _kernel.GetAll<IConcurrentWorldInitializer>();
            Parallel.ForEach(worldConcurrentInitializers, HandleConcurrentWorldInitializer);
            
            var worldTickers = _kernel.GetAll<IWorldTicker>();
            var concurrentWorldTickers = _kernel.GetAll<IConcurrentWorldTicker>();
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
            var mainMenuLoader = _kernel.Get<MainMenuLoader>();
            var mainMenu = mainMenuLoader.Load();
            World.SwitchScene(mainMenu);
        }
    }
}