using System;
using Ninject.Modules;

namespace ScientiaMagica.Common.Loader.Attributes {
    public class NamedInjectAttribute : Attribute, IPluginLoadAttribute {
        private readonly Type _bindingTarget;
        private readonly string _name;

        public NamedInjectAttribute(Type bindingTarget, string name) {
            _bindingTarget = bindingTarget;
            _name = name;
        }
        
        public void LoadType(NinjectModule module, Type holder) {
            module.Bind(_bindingTarget).To(holder).Named(_name);
        }
    }
}