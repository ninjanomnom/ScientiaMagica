using System;
using DryIoc;
using JetBrains.Annotations;
using ScientiaMagica.Common.Loader.Exceptions;

namespace ScientiaMagica.Common.Loader.Attributes {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [MeansImplicitUse]
    [UsedImplicitly]
    public class DefaultInjectAttribute : Attribute, IPluginLoadAttribute {
        private readonly Type _bindingTarget;
        
        public DefaultInjectAttribute(Type bindingTarget) {
            _bindingTarget = bindingTarget;
        }
        
        public void LoadType(IContainer container, Type holder) {
            if (!_bindingTarget.IsAssignableFrom(holder)) {
                throw new InvalidInjectionTypeException($"{holder} can not be bound to {_bindingTarget}");
            }

            container.Register(_bindingTarget, holder);
        }
    }
}