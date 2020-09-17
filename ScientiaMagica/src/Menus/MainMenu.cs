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
        
        private readonly Lazy<Button> _newButtonGetter;
        private Button NewButton => _newButtonGetter.Value;

        private readonly Lazy<Button> _loadButtonGetter;
        private Button LoadButton => _loadButtonGetter.Value;

        private readonly Lazy<Button> _exitButtonGetter;
        private Button ExitButton => _exitButtonGetter.Value;

        protected MainMenuImplementation() {
            _mainPanelGetter = new Lazy<Panel>(() => GetNode<Panel>("MainPanel"));
            _containerGetter = new Lazy<VBoxContainer>(() => MainPanel.GetNode<VBoxContainer>("MainContainer"));
            _newButtonGetter = new Lazy<Button>(() => Container.GetNode<Button>("NewButton"));
            _loadButtonGetter = new Lazy<Button>(() => Container.GetNode<Button>("LoadButton"));
            _exitButtonGetter = new Lazy<Button>(() => Container.GetNode<Button>("ExitButton"));

            Connect(nameof(MenuButtonRegistration), this, nameof(RegisterMenuButton));
        }
        
        public override void _Ready() {
            NewButton.Connect("pressed", this, nameof(NewPressed));
            ExitButton.Connect("pressed", this, nameof(ExitPressed));
            
            if (HasSaves()) {
                LoadButton.Disabled = false;
            }
        }

        [JetBrains.Annotations.Pure]
        private bool HasSaves() {
            return false;
        }

        private void NewPressed() {
            LoadButton.Disabled = !LoadButton.Disabled;
        }
        
        private void ExitPressed() {
            System.Environment.Exit(1);
        }

        private void RegisterMenuButton(Button button) {
            Container.AddChild(button);
        }
    }
}
