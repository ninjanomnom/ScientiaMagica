using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using ScientiaMagica.Common.Loader;
using ScientiaMagica.Common.Loader.Exceptions;

namespace ScientiaMagica.PluginLoader.Tests {
    [TestClass]
    public class PluginManagerTests {
        private IPluginManager _pluginManager;

        private int _basicPluginTracker = 1;
        
        [TestInitialize]
        public void Setup() {
            _pluginManager = new PluginManager();
        }

        private Mock<IPlugin> SetupBasicPlugin() {
            var info = new PluginIdentifier(
                $"Test{_basicPluginTracker}",
                $"Test{_basicPluginTracker}.Test",
                new Version(1, 0, 0)
            );
            _basicPluginTracker++;
            
            var mockedPlugin = new Mock<IPlugin>();
            mockedPlugin.Setup(p => p.Info).Returns(info);

            return mockedPlugin;
        }

        private static void CallAndUnwrapFirst(Action action) {
            try {
                action.Invoke();
            }
            catch (AggregateException e) {
                throw e.InnerExceptions[0];
            }
        }

        [TestMethod]
        public void PluginsGetInitialized() {
            var mockedPlugin = SetupBasicPlugin();
            _pluginManager.AddPlugin(mockedPlugin.Object);

            _pluginManager.LoadPlugins();
            
            mockedPlugin.Verify(p => p.Initialize(), Times.Exactly(1));
            mockedPlugin.Verify(p => p.LoadPlugin(It.IsAny<IDictionary<PluginIdentifier, IPlugin>>()), Times.Once);
        }
        
        [TestMethod]
        public void PluginsLoadInCorrectOrder() {
            var firstPlugin = SetupBasicPlugin();
            var secondPlugin = SetupBasicPlugin();
            var thirdPlugin = SetupBasicPlugin();

            secondPlugin.Setup(p => p.LoadAfter).Returns(new[] {firstPlugin.Object.Info});
            secondPlugin.Setup(p => p.LoadBefore).Returns(new[] {thirdPlugin.Object.Info});

            var loaded = new List<IPlugin>();
            firstPlugin.Setup(p => p.LoadPlugin(It.IsAny<IDictionary<PluginIdentifier, IPlugin>>()))
                .Callback<IDictionary<PluginIdentifier, IPlugin>>(dict => {
                    if (loaded.Any()) {
                        Assert.Fail("Some other plugin was loaded before firstPlugin");
                    }

                    loaded.Add(firstPlugin.Object);
                });

            secondPlugin.Setup(p => p.LoadPlugin(It.IsAny<IDictionary<PluginIdentifier, IPlugin>>()))
                .Callback<IDictionary<PluginIdentifier, IPlugin>>(dict => {
                    if (!loaded.Contains(firstPlugin.Object)) {
                        Assert.Fail("Second plugin is being loaded before first plugin");
                    }

                    loaded.Add(secondPlugin.Object);
                });

            thirdPlugin.Setup(p => p.LoadPlugin(It.IsAny<IDictionary<PluginIdentifier, IPlugin>>()))
                .Callback<IDictionary<PluginIdentifier, IPlugin>>(dict => {
                    if (!loaded.Contains(secondPlugin.Object)) {
                        Assert.Fail("Third plugin is being loaded before second plugin");
                    }

                    loaded.Add(secondPlugin.Object);
                });

            // Out of order so load order isn't based on add order
            _pluginManager.AddPlugin(thirdPlugin.Object);
            _pluginManager.AddPlugin(firstPlugin.Object);
            _pluginManager.AddPlugin(secondPlugin.Object);
            
            _pluginManager.LoadPlugins();
        }

        [TestMethod]
        public void AddingFailsAfterLoad() {
            var mockedPlugin = SetupBasicPlugin();
            _pluginManager.AddPlugin(mockedPlugin.Object);
            
            _pluginManager.LoadPlugins();

            Assert.ThrowsException<AlreadyInitializedException>(() =>
                _pluginManager.AddPlugin(SetupBasicPlugin().Object));
        }

        [TestMethod]
        public void PluginsRequireDependencies() {
            var unloadedInfo = new PluginIdentifier("Unloaded", "Unloaded.Test", new Version(1, 0, 0));
            
            var mockedPlugin = SetupBasicPlugin();
            mockedPlugin.Setup(p => p.Dependencies).Returns(new[] {unloadedInfo});
            
            _pluginManager.AddPlugin(mockedPlugin.Object);

            Assert.ThrowsException<MissingPluginException>(() => CallAndUnwrapFirst(() => _pluginManager.LoadPlugins()));
        }
    }
}