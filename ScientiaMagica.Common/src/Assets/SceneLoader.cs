using Godot;

namespace ScientiaMagica.Common.Assets {
    public class SceneLoader<T> where T : Node {
        private readonly PackedScene _packedScene;
        
        internal SceneLoader(string path) {
            _packedScene = ResourceLoader.Load<PackedScene>(path);
        }

        public T Instance() {
            return _packedScene.Instance() as T;
        }
    }
}