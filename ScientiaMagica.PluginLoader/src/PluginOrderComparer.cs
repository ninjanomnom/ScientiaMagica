using System.Collections.Generic;
using System.Linq;
using ScientiaMagica.Common.Loader;

namespace ScientiaMagica.PluginLoader {
    internal class PluginOrderComparer : IComparer<IPlugin> {
        public int Compare(IPlugin factoryA, IPlugin factoryB) {
            var oneNull = false;
            var output = 0;
                
            if (factoryA is null) {
                oneNull = true;
                output += 1;
            }

            if (factoryB is null) {
                oneNull = true;
                output -= 1;
            }

            if (output != 0 || oneNull) {
                return output;
            }

            if ((factoryA.LoadBefore?.Contains(factoryB.Info)).GetValueOrDefault(false)) {
                return -1;
            }
            
            if ((factoryB.LoadBefore?.Contains(factoryA.Info)).GetValueOrDefault(false)) {
                return +1;
            }
            
            if ((factoryA.LoadAfter?.Contains(factoryB.Info)).GetValueOrDefault(false)) {
                return +1;
            }
            
            if ((factoryB.LoadAfter?.Contains(factoryA.Info)).GetValueOrDefault(false)) {
                return -1;
            }

            return -1;
        }
    }
}