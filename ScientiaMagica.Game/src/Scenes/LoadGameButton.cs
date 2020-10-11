using System;
using Godot;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Loader.Attributes;
using ScientiaMagica.Common.Signals;

namespace ScientiaMagica.Game.Scenes {
    [DefaultInject(typeof(MainMenuButton))]
    public class LoadGameButton : MainMenuButton {
        private IMainMenu _mainMenu;
        
        public LoadGameButton() {
            this.InheritSceneStructure(Common.Assets.Scenes.LoadGameButton.Instance());
        }

        public override void _Ready() {
            Connect(ButtonSignals.Pressed, this, nameof(ButtonPressed));
        }

        public override void ConnectToMenu(IMainMenu mainMenu) {
            _mainMenu = mainMenu;
        }

        private void ButtonPressed() {
            _mainMenu.Hide();
            throw new NotImplementedException("Loading save games has not been implemented yet");
        }
    }
}