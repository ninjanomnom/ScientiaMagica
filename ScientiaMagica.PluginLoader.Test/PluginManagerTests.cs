using System;
using System.Collections.Generic;
using Moq;
using ScientiaMagica.Common.Loader;
using ScientiaMagica.Common.Loader.Exceptions;
using Xunit;

namespace ScientiaMagica.PluginLoader.Test {
    public class PluginManagerTests {
        private readonly IPluginManager _pluginManager;

        private int _basicPluginTracker = 1;
        
        public PluginManagerTests() {
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

        [Fact]
        public void PluginsGetInitialized() {
            var mockedPlugin = SetupBasicPlugin();
            _pluginManager.AddPlugin(mockedPlugin.Object);

            _pluginManager.LoadPlugins();
            
            mockedPlugin.Verify(p => p.Initialize(), Times.Exactly(1));
            mockedPlugin.Verify(p => p.LoadPlugin(It.IsAny<IDictionary<PluginIdentifier, IPlugin>>()), Times.Once);
        }
        
        [Fact]
        public void PluginsLoadInCorrectOrder() {
            var firstPlugin = SetupBasicPlugin();
            var secondPlugin = SetupBasicPlugin();
            var thirdPlugin = SetupBasicPlugin();

            secondPlugin.Setup(p => p.LoadAfter).Returns(new[] {firstPlugin.Object.Info});
            secondPlugin.Setup(p => p.LoadBefore).Returns(new[] {thirdPlugin.Object.Info});

            var loaded = new List<IPlugin>();
            firstPlugin.Setup(p => p.LoadPlugin(It.IsAny<IDictionary<PluginIdentifier, IPlugin>>()))
                .Callback<IDictionary<PluginIdentifier, IPlugin>>(dict => {
                    Assert.Empty(loaded);
                    
                    loaded.Add(firstPlugin.Object);
                });

            secondPlugin.Setup(p => p.LoadPlugin(It.IsAny<IDictionary<PluginIdentifier, IPlugin>>()))
                .Callback<IDictionary<PluginIdentifier, IPlugin>>(dict => {
                    Assert.Single(loaded);
                    Assert.Contains(firstPlugin.Object, loaded);
                    
                    loaded.Add(secondPlugin.Object);
                });

            thirdPlugin.Setup(p => p.LoadPlugin(It.IsAny<IDictionary<PluginIdentifier, IPlugin>>()))
                .Callback<IDictionary<PluginIdentifier, IPlugin>>(dict => {
                    Assert.Equal(2, loaded.Count);
                    Assert.Contains(firstPlugin.Object, loaded);
                    Assert.Contains(secondPlugin.Object, loaded);
                    
                    loaded.Add(secondPlugin.Object);
                });

            // Out of order so load order isn't based on add order
            _pluginManager.AddPlugin(thirdPlugin.Object);
            _pluginManager.AddPlugin(firstPlugin.Object);
            _pluginManager.AddPlugin(secondPlugin.Object);
            
            _pluginManager.LoadPlugins();
        }

        [Fact]
        public void AddingFailsAfterLoad() {
            var mockedPlugin = SetupBasicPlugin();
            _pluginManager.AddPlugin(mockedPlugin.Object);
            
            _pluginManager.LoadPlugins();

            Assert.Throws<AlreadyInitializedException>(() =>
                _pluginManager.AddPlugin(SetupBasicPlugin().Object));
        }

        [Fact]
        public void PluginsRequireDependencies() {
            var unloadedInfo = new PluginIdentifier("Unloaded", "Unloaded.Test", new Version(1, 0, 0));
            
            var mockedPlugin = SetupBasicPlugin();
            mockedPlugin.Setup(p => p.Dependencies).Returns(new[] {unloadedInfo});
            
            _pluginManager.AddPlugin(mockedPlugin.Object);

            Assert.Throws<MissingPluginException>(() => CallAndUnwrapFirst(() => _pluginManager.LoadPlugins()));
        }
    }
}