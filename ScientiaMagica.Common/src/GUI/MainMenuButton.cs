using Godot;
using JetBrains.Annotations;

namespace ScientiaMagica.Common.GUI {
    public abstract class MainMenuButton : Button {
        public virtual MainMenuPriorityOrder Priority { get; protected set; } = MainMenuPriorityOrder.Middle;

        public virtual void ConnectToMenu(IMainMenu mainMenu) { }
    }
}