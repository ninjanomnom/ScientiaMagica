using Godot;

namespace ScientiaMagica.Common.GUI {
    public class DefaultSceneLoader : ISceneLoader {
        private readonly PackedScene _scene;

        public DefaultSceneLoader(string nodeLocation) {
            _scene = ResourceLoader.Load<PackedScene>(nodeLocation);
        }
        
        public DefaultSceneLoader(string nodeLocation, string scriptLocation) {
            _scene = ResourceLoader.Load<PackedScene>(nodeLocation);
            _scene.SetScript(ResourceLoader.Load<Script>(scriptLocation));
        }
        
        public Node Load() {
            return _scene.Instance();
        }
    }
}