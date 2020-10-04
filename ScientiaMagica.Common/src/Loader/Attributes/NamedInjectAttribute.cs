using System;
using DryIoc;

namespace ScientiaMagica.Common.Loader.Attributes {
    public class NamedInjectAttribute : Attribute, IPluginLoadAttribute {
        private readonly Type _bindingTarget;
        private readonly string _name;

        public NamedInjectAttribute(Type bindingTarget, string name) {
            _bindingTarget = bindingTarget;
            _name = name;
        }
        
        public void LoadType(IContainer container, Type holder) {
            container.Register(_bindingTarget, holder);
        }
    }
}