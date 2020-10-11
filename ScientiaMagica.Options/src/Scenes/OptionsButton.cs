using System;
using ScientiaMagica.Common;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Loader.Attributes;
using ScientiaMagica.Common.Signals;

namespace ScientiaMagica.Options.Scenes {
    [DefaultInject(typeof(MainMenuButton))]
    public class OptionsButton : MainMenuButton {
        private readonly Func<OptionMenu> _menu;
        
        public OptionsButton(Func<OptionMenu> menu) {
            this.InheritSceneStructure(Common.Assets.Scenes.OptionsButton.Instance());
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
