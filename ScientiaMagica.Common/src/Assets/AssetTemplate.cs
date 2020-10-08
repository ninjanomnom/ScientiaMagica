using Godot;

namespace ScientiaMagica.Common.Assets {
    public struct Scenes {
        public static readonly SceneLoader<Node2D> Main = new SceneLoader<Node2D>("res://ScientiaMagica/src/Main.tscn");
		public static readonly SceneLoader<Button> ExitButton = new SceneLoader<Button>("res://ScientiaMagica/src/Menus/ExitButton.tscn");
		public static readonly SceneLoader<CanvasLayer> MainMenu = new SceneLoader<CanvasLayer>("res://ScientiaMagica/src/Menus/MainMenu.tscn");
		public static readonly SceneLoader<Button> LoadGameButton = new SceneLoader<Button>("res://ScientiaMagica.Game/src/Scenes/LoadGameButton.tscn");
		public static readonly SceneLoader<Button> NewGameButton = new SceneLoader<Button>("res://ScientiaMagica.Game/src/Scenes/NewGameButton.tscn");
		public static readonly SceneLoader<RigidBody2D> Player = new SceneLoader<RigidBody2D>("res://ScientiaMagica.Game/src/Scenes/Player.tscn");
		public static readonly SceneLoader<Button> OptionsButton = new SceneLoader<Button>("res://ScientiaMagica.Options/src/Scenes/OptionsButton.tscn");
		public static readonly SceneLoader<Panel> OptionsCategory = new SceneLoader<Panel>("res://ScientiaMagica.Options/src/Scenes/OptionsCategory.tscn");
		public static readonly SceneLoader<CanvasLayer> OptionsMenu = new SceneLoader<CanvasLayer>("res://ScientiaMagica.Options/src/Scenes/OptionsMenu.tscn");    
    }
}