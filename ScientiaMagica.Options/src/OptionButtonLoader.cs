using Godot;
using Ninject;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Loader.Attributes;
using ScientiaMagica.Common.Resources;

namespace ScientiaMagica.Options {
    [DefaultInject(typeof(MainMenuButton))]
    public class OptionButtonLoader : MainMenuButton {
        public override PriorityOrder Priority { get; } = PriorityOrder.OverExit;
        protected override ISceneLoader Loader { get; } = new DefaultSceneLoader(@"res://ScientiaMagica.Options/src/Scenes/OptionButton.tscn");

        public OptionButtonLoader() {
            Loader = new NodeLoader("res://ScientiaMagica.Options/src/Scenes/OptionButton.tscn");
        }
    }
}