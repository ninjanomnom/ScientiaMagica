using Godot;
using JetBrains.Annotations;
using ScientiaMagica.Common.GUI;

namespace ScientiaMagica.Common.Resources {
    public class NodeLoader : ISceneLoader {
        private readonly PackedScene _node;
        
        public NodeLoader([PathReference] string location) {
            var scene = ResourceLoader.Load(location);
            _node = (PackedScene)scene;
        }

        public virtual Node Load() {
            return _node.Instance();
        }
    }
}