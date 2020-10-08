using Godot;
using ScientiaMagica.Common.GodotExtensions;

namespace ScientiaMagica.Game.Scenes {
    public class Player : RigidBody2D {
        private Vector2? _queuedNewLocation;
        
        public Player() {
            this.InheritSceneStructure(Common.Assets.Scenes.Player.Instance());
        }
        
        public override void _Ready() {
        }

        public override void _Process(float delta) {
        }

        public override void _IntegrateForces(Physics2DDirectBodyState state) {
            if (_queuedNewLocation.HasValue) {
                Mode = ModeEnum.Kinematic;
                Position = _queuedNewLocation.Value;
            } else if (Mode == ModeEnum.Kinematic) {
                Mode = ModeEnum.Rigid;
            }
        }

        public void QueuePosition(Vector2 newLoc) {
            _queuedNewLocation = newLoc;
        }
    }
}