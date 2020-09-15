using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Ninject;

namespace ScientiaMagica.Common.Loader {
    /// <summary>
    /// Don't use this directly unless you know what you're doing<br/>
    /// <see cref="DefaultPlugin"/> should be used for most purposes
    /// </summary>
    public abstract class PluginFactory {
        [NotNull]
        public PluginIdentifier Info { get; protected set; }
        [CanBeNull]
        public IEnumerable<PluginIdentifier> Dependencies { get; protected set; }
        [CanBeNull]
        public IEnumerable<PluginIdentifier> Incompatible { get; protected set; }
        [CanBeNull]
        public IEnumerable<PluginIdentifier> LoadAfter { get; protected set; }
        [CanBeNull]
        public IEnumerable<PluginIdentifier> LoadBefore { get; protected set; }

        public abstract void Initialize(IKernel kernel);
        [NotNull]
        public abstract IPlugin Get();
    }
    
    public sealed class PluginFactory<T> : PluginFactory where T : IPlugin {
        private IPlugin _initializedPlugin;
        
        public PluginFactory(PluginIdentifier info,
            IEnumerable<PluginIdentifier> dependencies = null,
            IEnumerable<PluginIdentifier> incompatible = null) {
            Info = info;
            Dependencies = dependencies ?? new PluginIdentifier[] { };
            Incompatible = incompatible ?? new PluginIdentifier[] { };
        }

        public override void Initialize(IKernel kernel) {
            _initializedPlugin = kernel.Get<T>();
        }

        public override IPlugin Get() {
            if (_initializedPlugin is null) {
                // TODO: Not initialized exception
                throw new NotImplementedException();
            }

            return _initializedPlugin;
        }
    }
}