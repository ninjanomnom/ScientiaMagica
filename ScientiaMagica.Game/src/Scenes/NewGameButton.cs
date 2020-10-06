﻿using System;
using Godot;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.Signals;
using Thread = System.Threading.Thread;

namespace ScientiaMagica.Game.Scenes {
    public class NewGameButton : Button {
        private readonly WindowBoundingBox _windowBounds;
        private readonly Lazy<Player> _player;
        
        public NewGameButton(WindowBoundingBox windowBounds, Lazy<Player> player) {
            this.InheritSceneStructure(@"res://ScientiaMagica.Game/src/Scenes/NewGameButton.tscn");
            
            _windowBounds = windowBounds;
            _player = player;
        }

        public override void _Ready() {
            Connect(ButtonSignals.Pressed, this, nameof(ButtonPressed));
        }

        private void ButtonPressed() {
            Common.World.MainNode.AddChild(_windowBounds);
            
            var initialPosition = new Vector2(50, 50).Normalized();
            Common.World.MainNode.AddChild(_player.Value);
            _player.Value.CallDeferred("set", "position", initialPosition);
        }
    }
}