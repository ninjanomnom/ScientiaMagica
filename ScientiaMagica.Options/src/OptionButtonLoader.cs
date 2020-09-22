using System.Collections;
using Godot;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Loader.Attributes;
using ScientiaMagica.Options.Scenes;

namespace ScientiaMagica.Options {
    [DefaultInject(typeof(IMainMenuButton))]
    public class OptionButtonLoader : IMainMenuButton {
        public MainMenuPriorityOrder Priority { get; } = MainMenuPriorityOrder.OverExit;

        private readonly Button _button;

        public OptionButtonLoader(OptionsButton button) {
            button.SetPropertiesFromScene(@"res://ScientiaMagica.Options/src/Scenes/OptionsButton.tscn");
            _button = button;
        }
        
        public Button GetButton() {
            return _button;
        }
    }
}