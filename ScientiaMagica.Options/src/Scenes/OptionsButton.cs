using System;
using Godot;
using JetBrains.Annotations;
using ScientiaMagica.Common.Signals;
using World = ScientiaMagica.Common.World;

namespace ScientiaMagica.Options.Scenes {
    [UsedImplicitly]
    public class OptionsButton : Button {
        private readonly Func<OptionMenu> _menu;
        
        public OptionsButton(Func<OptionMenu> menu) {
            _menu = menu;
        }
        
        public override void _Ready() {
            Connect(ButtonSignals.Pressed, this, nameof(ButtonPressed));
        }

        private void ButtonPressed() {
            World.MainNode.AddChild(_menu.Invoke());
        }
    }
}
