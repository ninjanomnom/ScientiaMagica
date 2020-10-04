using System.Collections.Generic;
using Godot;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.GUI;

namespace ScientiaMagica.Menus {
    public class MainMenu : CanvasLayer {
        public MainMenu() : this(new IMainMenuButton[] { }) { }
        
        public MainMenu(IEnumerable<IMainMenuButton> buttons) {
            this.InheritSceneStructure(@"res://ScientiaMagica/src/Menus/MainMenu.tscn");
            
            var container = GetNode<VBoxContainer>("MainPanel/MainContainer");

            foreach (var button in buttons) {
                container.AddChild(button.GetButton());
            }
        }
    }
}
