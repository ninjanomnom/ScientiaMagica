using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Menus;
using Thread = System.Threading.Thread;

[UsedImplicitly]
public class MainMenu : MainMenuImplementation { }

namespace ScientiaMagica.Menus {
    public class MainMenuImplementation : CanvasLayer {
        [Signal]
        public delegate void MenuButtonRegistration(Button button);
        
        private readonly Lazy<Panel> _mainPanelGetter;
        private Panel MainPanel => _mainPanelGetter.Value;
        
        private readonly Lazy<VBoxContainer> _containerGetter;
        private VBoxContainer Container => _containerGetter.Value;

        protected MainMenuImplementation() {
            _mainPanelGetter = new Lazy<Panel>(() => GetNode<Panel>("MainPanel"));
            _containerGetter = new Lazy<VBoxContainer>(() => MainPanel.GetNode<VBoxContainer>("MainContainer"));

            Connect(nameof(MenuButtonRegistration), this, nameof(RegisterMenuButton));
        }

        private void RegisterMenuButton(Button button) {
            Container.AddChild(button);
        }
    }
}
