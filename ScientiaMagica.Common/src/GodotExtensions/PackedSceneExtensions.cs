using System;
using Godot;

namespace ScientiaMagica.Common.GodotExtensions {
    public static class PackedSceneExtensions {
        public static T Instance<T>(this PackedScene src) where T : Node {
            return (T) src.Instance();
        }
    }
}