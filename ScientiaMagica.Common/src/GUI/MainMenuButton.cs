using System;
using Godot;
using ScientiaMagica.Common.GodotExtensions;

namespace ScientiaMagica.Common.GUI {
    public abstract class MainMenuButton {
        public abstract PriorityOrder Priority { get; }
        public enum PriorityOrder {
            OverNewGame,
            AtNewGame,
            OverLoad,
            AtLoad,
            OverExit,
            AtExit,
            UnderExit
        }
        
        protected abstract ISceneLoader Loader { get; }
        
        public Button GetButton() {
            var button = Loader.LoadScene();
            if (!button.Is<Button>()) {
                // TODO: Not a button exception
                throw new NotImplementedException("Not a button");
            }

            return (Button)button;
        }
    }
}