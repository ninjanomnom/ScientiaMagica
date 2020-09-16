using System.Collections.Generic;
using JetBrains.Annotations;
using Ninject.Modules;
using Ninject.Parameters;

namespace ScientiaMagica.Common.Loader {
    /// <summary>
    /// The base version of <see cref="InjectedPlugin{T}"/>, use the other instead of this one
    /// </summary>
    public abstract class InjectedPlugin : NinjectModule, IPlugin {
        public abstract PluginIdentifier Info { get; }

        public virtual IEnumerable<PluginIdentifier> Dependencies { get; } = null;
        public virtual IEnumerable<PluginIdentifier> Incompatible { get; } = null;
        public virtual IEnumerable<PluginIdentifier> LoadAfter { get; } = null;
        public virtual IEnumerable<PluginIdentifier> LoadBefore { get; } = null;

        public virtual void Initialize() { }

        public virtual void LoadPlugin(IDictionary<PluginIdentifier, IPlugin> plugins) { }
    }
    
    /// <summary>
    /// The core plugin object<br/>
    /// This handles everything from getting registered to dependency analysis<br/>
    /// </summary>
    /// <typeparam name="T">When inheriting from this, the generic argument should be the type that is implementing <see cref="InjectedPlugin{T}"/>
    /// For example: <code>public class MyPlugin : DefaultPlugin&#60;MyPlugin&#62; {...}</code></typeparam>
    [PublicAPI]
    public abstract class InjectedPlugin<T> : InjectedPlugin where T : InjectedPlugin {
        public sealed override void Load() {
            Bind<IPlugin>().ToConstant(this);
        }
    }
}