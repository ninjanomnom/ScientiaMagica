using Godot;
using JetBrains.Annotations;
using ScientiaMagica.Common.GUI;

namespace ScientiaMagica.Common.GodotExtensions {
    public static class GodotNodeExtensions {
        /// <summary>
        /// Copies all properties from another node to this one
        /// </summary>
        /// <param name="src">The receiver of the new properties</param>
        /// <param name="sceneObject">The source of the new properties</param>
        [PublicAPI]
        public static void SetPropertiesFromScene(this Node src, Node sceneObject) {
            sceneObject.CopyProperties().To(src);
        }

        /// <summary>
        /// Copies all properties from an unloaded scene node to this one
        /// </summary>
        /// <param name="src">The receiver of the new properties</param>
        /// <param name="sceneLocation">The location of the source of the new properties</param>
        [PublicAPI]
        public static void SetPropertiesFromScene(this Node src, string sceneLocation) {
            var scene = ResourceLoader.Load<PackedScene>(sceneLocation).Instance();
            src.SetPropertiesFromScene(scene);
        }

        /// <summary>
        /// Copies all properties from an unloaded scene node to this one
        /// </summary>
        /// <param name="src">The receiver of the new properties</param>
        /// <param name="loader">The unloaded scene</param>
        [PublicAPI]
        public static void SetPropertiesFromScene(this Node src, ISceneLoader loader) {
            src.SetPropertiesFromScene(loader.Load());
        }

        /// <summary>
        /// Moves all child nodes from this node to another
        /// </summary>
        /// <param name="src">The source of all children to be moved</param>
        /// <param name="target">The new parent of the moved children</param>
        [PublicAPI]
        public static void MoveAllChildren(this Node src, Node target) {
            foreach (var child in src.GetChildren()) {
                var childNode = (Node) child;
                src.RemoveChild(childNode);
                target.AddChild(childNode);
            }
        }

        /// <summary>
        /// Copies all data in a scene file to a node of the same type
        /// </summary>
        /// <param name="src">The node to take on the new data</param>
        /// <param name="sceneLocation">The source of the data</param>
        [PublicAPI]
        public static void InheritSceneStructure(this Node src, string sceneLocation) {
            var scene = ResourceLoader.Load<PackedScene>(sceneLocation).Instance();
            src.SetPropertiesFromScene(scene);
            scene.MoveAllChildren(src);
        }
    }
}