using Godot;

namespace ScientiaMagica.Common.Resources {
    public class ScriptLoader {
        private readonly Script _script = null;
        
        public ScriptLoader(string location) {
            if (location != null) {
                _script = ResourceLoader.Load<Script>(location);
            }
        }

        public Script Get() {
            return _script;
        }
    }
}