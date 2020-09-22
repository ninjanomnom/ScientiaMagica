using System;
using System.Collections.Generic;
using Godot;
using JetBrains.Annotations;

namespace ScientiaMagica.Common {
    public static class World {
        public static string CompatibilityHash { get; internal set; }
        public static string FullHash { get; internal set; }
        
        [PublicAPI]
        public static Node MainNode { get; private set; }
        
        [PublicAPI]
        public static Node MainScene { get; private set; }

        public static void Initialize(Node mainNode) {
            if (MainNode != null) {
                throw new InvalidOperationException("The world has already been initialized with a MainNode.");
            }
            
            MainNode = mainNode;
        }
        
        public static void SwitchScene(Node newRoot) {
            if (newRoot != null) {
                MainNode.AddChild(newRoot);
            }
            
            if (MainScene != null) {
                MainNode.RemoveChild(MainScene);
            }
            
            MainScene = newRoot;
        }
    }
}