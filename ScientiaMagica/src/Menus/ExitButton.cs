using Godot;
using JetBrains.Annotations;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.Signals;

namespace ScientiaMagica.Menus {
    [UsedImplicitly]
    public class ExitButton : Button {
        public ExitButton() {
            this.SetPropertiesFromScene(@"res://ScientiaMagica/src/Menus/ExitButton.tscn");
        }
        
        public override void _Ready() {
            Connect(ButtonSignals.Pressed, this, nameof(ButtonPressed));
        }

        private void ButtonPressed() {
            System.Environment.Exit(0);
        }
    }
}