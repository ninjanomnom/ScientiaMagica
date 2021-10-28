using System;
using Godot;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Loader.Attributes;
using ScientiaMagica.Common.Signals;
using Thread = System.Threading.Thread;

namespace ScientiaMagica.Game.Scenes {
    [DefaultInject(typeof(MainMenuButton))]
    public class NewGameButton : MainMenuButton {
        public override MainMenuPriorityOrder Priority { get; protected set; } = MainMenuPriorityOrder.Top;

        private readonly WindowBoundingBox _windowBounds;
        private readonly Lazy<Player> _player;
        private IMainMenu _mainMenu;
        
        public NewGameButton(WindowBoundingBox windowBounds, Lazy<Player> player) {
            this.InheritSceneStructure(Common.Assets.Scenes.NewGameButton.Instance());
            
            _windowBounds = windowBounds;
            _player = player;
        }

        public override void _Ready() {
            Connect(ButtonSignals.Pressed, this, nameof(ButtonPressed));
        }

        public override void ConnectToMenu(IMainMenu mainMenu) {
            _mainMenu = mainMenu;
        }

        private void ButtonPressed() {
            _mainMenu.Hide();
            
            Common.World.MainNode.AddChild(_windowBounds);
            
            var initialPosition = new Vector2(50, 50);
            _player.Value.QueuePosition(initialPosition);
            Common.World.MainNode.AddChild(_player.Value);
        }
    }
}