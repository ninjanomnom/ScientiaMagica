using Godot;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Loader.Attributes;

namespace ScientiaMagica.Menus {
    [DefaultInject(typeof(IMainMenuButton))]
    public class ExitButtonLoader : IMainMenuButton {
        private readonly ExitButton _exitButton;

        public ExitButtonLoader(ExitButton button) {
            _exitButton = button;
        }

        public MainMenuPriorityOrder Priority { get; } = MainMenuPriorityOrder.End;
        
        public Button GetButton() {
            return _exitButton;
        }
    }
}