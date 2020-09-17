using System;
using JetBrains.Annotations;
using Ninject.Modules;

namespace ScientiaMagica.Common.Loader.Attributes {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [MeansImplicitUse]
    [UsedImplicitly]
    public class DefaultInjectAttribute : Attribute, IPluginLoadAttribute {
        private readonly Type _bindingTarget;
        
        public DefaultInjectAttribute(Type bindingTarget) {
            _bindingTarget = bindingTarget;
        }
        
        public void LoadType(NinjectModule module, Type holder) {
            if (!_bindingTarget.IsAssignableFrom(holder)) {
                // TODO: Throw applied to wrong type
                throw new NotImplementedException();
            }

            module.Bind(_bindingTarget).To(holder);
        }
    }
}