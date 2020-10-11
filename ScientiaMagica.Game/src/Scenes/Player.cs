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
            if (!_queuedNewLocation.HasValue) {
                return;
            }
            
            Position = _queuedNewLocation.Value;
            _queuedNewLocation = null;
            Mode = ModeEnum.Rigid;
        }

        public void QueuePosition(Vector2 newLoc) {
            _queuedNewLocation = newLoc;
            Mode = ModeEnum.Static;
        }
    }
}