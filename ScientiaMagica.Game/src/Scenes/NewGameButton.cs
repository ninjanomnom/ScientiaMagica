using Godot;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.Signals;

namespace ScientiaMagica.Game.Scenes {
    public class NewGameButton : Button {
        public NewGameButton() {
            this.SetPropertiesFromScene(@"res://ScientiaMagica.Game/src/Scenes/NewGameButton.tscn");
            // TODO: Assign new game menu here
        }

        public override void _Ready() {
            Connect(ButtonSignals.Pressed, this, nameof(ButtonPressed));
        }

        private void ButtonPressed() {
            // TODO: World.SwitchScene(_menu);
        }
    }
}