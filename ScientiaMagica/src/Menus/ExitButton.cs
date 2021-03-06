﻿using Godot;
using JetBrains.Annotations;
using ScientiaMagica.Common.Assets;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Loader.Attributes;
using ScientiaMagica.Common.Signals;

namespace ScientiaMagica.Menus {
    [DefaultInject(typeof(MainMenuButton))]
    public class ExitButton : MainMenuButton {
        public override MainMenuPriorityOrder Priority { get; protected set; } = MainMenuPriorityOrder.End;

        public ExitButton() {
            this.InheritSceneStructure(Scenes.ExitButton.Instance());
        }
        
        public override void _Ready() {
            Connect(ButtonSignals.Pressed, this, nameof(ButtonPressed));
        }

        private void ButtonPressed() {
            System.Environment.Exit(0);
        }
    }
}