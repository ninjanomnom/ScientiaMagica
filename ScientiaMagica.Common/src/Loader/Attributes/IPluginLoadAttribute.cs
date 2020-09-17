using System;
using Ninject.Modules;

namespace ScientiaMagica.Common.Loader.Attributes {
    public interface IPluginLoadAttribute {
        /// <summary>
        /// Called for each type that has the attribute applied to it
        /// </summary>
        /// <param name="module">The module to use for binding</param>
        /// <param name="holder">The type this attribute has been applied to</param>
        void LoadType(NinjectModule module, Type holder);
    }
}