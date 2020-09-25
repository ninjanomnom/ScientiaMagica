using Godot;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Loader.Attributes;
using ScientiaMagica.Game.Scenes;

namespace ScientiaMagica.Game {
    [DefaultInject(typeof(IMainMenuButton))]
    public class NewGameButtonLoader : IMainMenuButton {
        public MainMenuPriorityOrder Priority { get; } = MainMenuPriorityOrder.Top;

        private readonly Button _button;

        public NewGameButtonLoader(NewGameButton button) {
            _button = button;
        }
        
        public Button GetButton() {
            return _button;
        }
    }

    [DefaultInject(typeof(IMainMenuButton))]
    public class LoadGameButtonLoader : IMainMenuButton {
        public MainMenuPriorityOrder Priority { get; } = MainMenuPriorityOrder.Middle;

        private readonly Button _button;
        
        public LoadGameButtonLoader(LoadGameButton button) {
            _button = button;
        }
        
        public Button GetButton() {
            return _button;
        }
    }
}