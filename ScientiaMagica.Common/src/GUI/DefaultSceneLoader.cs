using Godot;

namespace ScientiaMagica.Common.GUI {
    public class DefaultSceneLoader : ISceneLoader {
        private readonly Node _scene;
        
        public DefaultSceneLoader(string nodeLocation, string scriptLocation = null) {
            _scene = ResourceLoader.Load<PackedScene>(nodeLocation).Instance();
            if (scriptLocation != null) {
                _scene.SetScript(ResourceLoader.Load<Script>(scriptLocation));
            }
        }
        
        public Node LoadScene() {
            return _scene;
        }
    }
}