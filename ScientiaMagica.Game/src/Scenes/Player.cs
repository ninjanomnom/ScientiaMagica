using Godot;
using ScientiaMagica.Common.GodotExtensions;

namespace ScientiaMagica.Game.Scenes {
    public class Player : RigidBody2D {
        public Player() {
            this.InheritSceneStructure(Common.Assets.Scenes.Player.Instance());
        }
        
        public override void _Ready() {
        }

        public override void _Process(float delta) {
            if (Mode == ModeEnum.Static) {
                Mode = ModeEnum.Rigid;
            }
        }

        public override void _IntegrateForces(Physics2DDirectBodyState state) {
            HandlePlayerInput(state);
        }

        public void QueuePosition(Vector2 newLoc) {
            Mode = ModeEnum.Static;
            Position = newLoc;
        }

        private void HandlePlayerInput(Physics2DDirectBodyState state) {
            var x = 0;
            var y = 0;
            
            if (Input.IsActionPressed(Directions.North)) {
                y += 1;
            }
            
            if (Input.IsActionPressed(Directions.East)) {
                x += 1;
            }

            if (Input.IsActionPressed(Directions.South)) {
                y -= 1;
            }

            if (Input.IsActionPressed(Directions.West)) {
                x -= 1;
            }
            
            state.AddForce(new Vector2(), new Vector2(x, y).Normalized());
        }
        
        private struct Directions {
            public const string North = "ui_up";
            public const string East = "ui_right";
            public const string South = "ui_down";
            public const string West = "ui_left";
        }
    }
}