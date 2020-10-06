using Godot;
using ScientiaMagica.Common.GodotExtensions;

namespace ScientiaMagica.Game.Scenes {
    public class Player : RigidBody2D {
        public Player() {
            this.InheritSceneStructure(@"res://ScientiaMagica.Game/src/Scenes/Player.tscn");
        }
        
        public override void _Ready() {
        }

        public override void _Process(float delta) {
        }

        private void UpdatePosition(float delta) {
            
        }
    }
}