using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using ScientiaMagica.Common.Assets;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.GUI;

namespace ScientiaMagica.Menus {
    public class MainMenu : CanvasLayer, IMainMenu {
        private readonly Panel _mainPanel;
        
        public MainMenu(IEnumerable<MainMenuButton> buttons) {
            this.InheritSceneStructure(Scenes.MainMenu.Instance());
            
            _mainPanel = GetNode<Panel>("MainPanel");
            var container = _mainPanel.GetNode<VBoxContainer>("MainContainer");

            foreach (var button in buttons.OrderBy(b => b.Priority)) {
                button.ConnectToMenu(this);
                container.AddChild(button);
            }
        }

        public void Hide() {
            _mainPanel.Hide();
        }
        
        public void Show() {
            _mainPanel.Show();
        }
    }
}
