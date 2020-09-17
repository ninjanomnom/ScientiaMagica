using Godot;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Loader.Attributes;

namespace ScientiaMagica.Options {
    [DefaultInject(typeof(MainMenuButton))]
    public class OptionButton : MainMenuButton {
        public override PriorityOrder Priority { get; } = PriorityOrder.OverExit;
        protected override ISceneLoader Loader { get; } = new DefaultSceneLoader(@"res://ScientiaMagica.Options/src/Scenes/OptionButton.tscn");
    }
}