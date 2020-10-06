﻿using System;
using Godot;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.Signals;

namespace ScientiaMagica.Game.Scenes {
    public class LoadGameButton : Button {
        public LoadGameButton() {
            this.SetPropertiesFromScene(@"res://ScientiaMagica.Game/src/Scenes/LoadGameButton.tscn");
            // TODO: Assign laod game menu here
        }

        public override void _Ready() {
            Connect(ButtonSignals.Pressed, this, nameof(ButtonPressed));
        }

        private void ButtonPressed() {
            // TODO: World.SwitchScene(_menu);
            throw new NotImplementedException("Loading save games has not been implemented yet");
        }
    }
}