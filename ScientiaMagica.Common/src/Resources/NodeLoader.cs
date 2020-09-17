using Godot;
using ScientiaMagica.Common.GUI;

namespace ScientiaMagica.Common.Resources {
    public class NodeLoader : ISceneLoader {
        private readonly PackedScene _node;
        
        public NodeLoader(string location) {
            var scene = Godot.ResourceLoader.Load<PackedScene>(location);
            _node = scene;
        }

        public Node Load() {
            return _node.Instance();
        }
    }
}