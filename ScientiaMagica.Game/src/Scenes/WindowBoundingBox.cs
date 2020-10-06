using Godot;
using ScientiaMagica.Common.Signals;

namespace ScientiaMagica.Game.Scenes {
    public class WindowBoundingBox : Area2D {
        private readonly CollisionShape2D _bounds;
        
        public WindowBoundingBox() {
            SpaceOverride = SpaceOverrideEnum.Replace;
            Gravity = 0;
            
            _bounds = new CollisionShape2D();
        }

        public override void _Ready() {
            _bounds.Shape = new RectangleShape2D {
                Extents = GetViewport().Size
            };

            Connect(Area2DSignals.BodyEntered, this, nameof(BodyEntered));
            Connect(Area2DSignals.BodyExited, this, nameof(BodyExited));
        }

        private void BodyEntered(Node other) {
        }

        private void BodyExited(Node other) {
        }
    }
}