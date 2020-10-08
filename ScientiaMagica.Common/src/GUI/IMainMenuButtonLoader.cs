using Godot;

namespace ScientiaMagica.Common.GUI {
    public interface IMainMenuButtonLoader {
        MainMenuPriorityOrder Priority { get; }

        Button GetButton();
    }
}