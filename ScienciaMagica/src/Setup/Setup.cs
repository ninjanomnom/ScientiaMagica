using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using Ninject;
using NLog;
using ScientiaMagica.Common.Loader;
using ScientiaMagica.Common.Loader.Controllers;
using ScientiaMagica.Common.Loader.Exceptions;

namespace ScientiaMagica.Setup {
    public class Setup {
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
            foreach (var factory in _kernel.GetAll<PluginFactory>()) {
                pluginManager.AddPlugin(factory);
            }
            pluginManager.LoadPlugins(_kernel);
        }
        
        private void LoadDependencies() {
            _kernel.Load("ScientiaMagica.*.dll", "Plugins/*/*.Main.dll");
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
    }
}