using Godot;

namespace ScientiaMagica.Common.GUI {
    public interface IMainMenuButton {
        MainMenuPriorityOrder Priority { get; }

        Button GetButton();
    }
}