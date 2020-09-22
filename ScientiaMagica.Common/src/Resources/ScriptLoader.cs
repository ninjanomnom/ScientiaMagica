using Godot;
using JetBrains.Annotations;

namespace ScientiaMagica.Common.Resources {
    public class ScriptLoader {
        private readonly Script _script = null;
        
        public ScriptLoader([PathReference] string location) {
            if (location != null) {
                _script = ResourceLoader.Load<Script>(location);
            }
        }

        public Script Get() {
            return _script;
        }
    }
}