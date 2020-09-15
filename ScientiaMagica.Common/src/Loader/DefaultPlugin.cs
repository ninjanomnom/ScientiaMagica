using System.Collections.Generic;
using JetBrains.Annotations;
using Ninject.Modules;

namespace ScientiaMagica.Common.Loader {
    /// <summary>
    /// The base version of <see cref="DefaultPlugin{T}"/>, use the other instead of this one
    /// </summary>
    public abstract class DefaultPlugin : NinjectModule, IPlugin {
        public abstract PluginIdentifier Info { get; }

        public abstract void LoadPlugin(IDictionary<PluginIdentifier, IPlugin> plugins);
    }
    
    /// <summary>
    /// The core plugin object<br/>
    /// This handles everything from getting registered to dependency analysis<br/>
    /// </summary>
    /// <typeparam name="T">When inheriting from this, the generic argument should be the type that is implementing <see cref="DefaultPlugin{T}"/>
    /// For example: <code>public class MyPlugin : DefaultPlugin&#60;MyPlugin&#62; {...}</code></typeparam>
    [UsedImplicitly]
    public abstract class DefaultPlugin<T> : DefaultPlugin where T : DefaultPlugin {
        public sealed override void Load() {
            Bind<PluginFactory>().To<PluginFactory<T>>();
        }
    }
}