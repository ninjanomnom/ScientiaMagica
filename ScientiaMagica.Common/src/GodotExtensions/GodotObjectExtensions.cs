using Godot;

namespace ScientiaMagica.Common.GodotExtensions {
    public static class GodotObjectExtensions {
        public static bool Is<T>(this Godot.Object src) where T : Node {
            return src.IsClass(typeof(T).Name);
        }
    }
}