using System.Collections.Generic;
using System.Linq;
using Godot;
using ScientiaMagica.Common.Assets;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.GUI;

namespace ScientiaMagica.Menus {
    public class MainMenu : CanvasLayer {
        public MainMenu() : this(new IMainMenuButtonLoader[] { }) { }
        
        public MainMenu(IEnumerable<IMainMenuButtonLoader> buttons) {
            this.InheritSceneStructure(Scenes.MainMenu.Instance());

            var container = GetNode<VBoxContainer>("MainPanel/MainContainer");

            foreach (var button in buttons.OrderBy(b => b.Priority)) {
                container.AddChild(button.GetButton());
            }
        }
    }
}
