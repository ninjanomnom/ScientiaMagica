using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DryIoc;
using ScientiaMagica.Common.Loader.Attributes;

namespace ScientiaMagica.Common.Loader {
    /// <summary>
    /// The core plugin object<br/>
    /// This handles everything from getting registered to dependency analysis<br/>
    /// </summary>
    public abstract class InjectedPlugin : IPlugin {
        public abstract PluginIdentifier Info { get; }

        public virtual IEnumerable<PluginIdentifier> Dependencies { get; } = null;
        public virtual IEnumerable<PluginIdentifier> Incompatible { get; } = null;
        public virtual IEnumerable<PluginIdentifier> LoadAfter { get; } = null;
        public virtual IEnumerable<PluginIdentifier> LoadBefore { get; } = null;

        public virtual void Initialize() { }

        public virtual void LoadPlugin(IDictionary<PluginIdentifier, IPlugin> plugins) { }
        
        public void Load(IContainer container) {
            container.UseInstance<IPlugin>(this);

            var ourAssembly = Assembly.GetAssembly(GetType());

            foreach (var holder in ourAssembly.GetTypes()) {
                var loaderAttributes = Attribute
                    .GetCustomAttributes(holder)
                    .OfType<IPluginLoadAttribute>();
                foreach (var attribute in loaderAttributes) {
                    attribute.LoadType(container, holder);
                }
            }
        }
    }
}