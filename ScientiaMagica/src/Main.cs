using System;
using Godot;
using JetBrains.Annotations;
using NLog;
using ScientiaMagica;
using ScientiaMagica.Common.GUI;

[UsedImplicitly]
public class Main : MainImplementation { };

namespace ScientiaMagica {
    public class MainImplementation : Node2D {
        public override void _Ready() {
            ScientiaMagica.World.MainNode = this;
            
            var setup = new Setup.Setup();
            var logger = LogManager.GetCurrentClassLogger();
            try {
                setup.LoadPlugins();
            }
            catch (Exception e) {
                logger.Fatal(e, "An unknown exception occured while loading plugins");
                throw;
            }

            try {
                setup.InitializeGame();
            }
            catch (Exception e) {
                logger.Fatal(e, "An unknown exception occured while initializing the game");
                throw;
            }
            
            setup.LoadMenu();
        }
    }
}