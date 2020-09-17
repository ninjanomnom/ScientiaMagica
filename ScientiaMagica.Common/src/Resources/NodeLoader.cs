using Godot;

namespace ScientiaMagica.Common.Resources {
    public class NodeLoader {
        private readonly Node _node;
        
        public NodeLoader(string location) {
            var scene = Godot.ResourceLoader.Load<PackedScene>(location);
            _node = scene.Instance();
        }

        public Node Get() {
            return _node;
        }
    }
}