using Godot;

namespace ScientiaMagica {
    public static class World {
        public static string CompatibilityHash { get; internal set; }
        public static string FullHash { get; internal set; }
        
        public static Node MainNode { get; internal set; }
    }
}