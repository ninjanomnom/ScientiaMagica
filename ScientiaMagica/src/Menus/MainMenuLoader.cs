using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using JetBrains.Annotations;
using Ninject;
using ScientiaMagica.Common.GUI;
using ScientiaMagica.Common.Resources;

namespace ScientiaMagica.Menus {
    [UsedImplicitly]
    public class MainMenuLoader : ISceneLoader {
        private readonly NodeLoader _nodeLoader;
        private readonly ScriptLoader _scriptLoader;
        private readonly IEnumerable<IMainMenuButton> _menuButtons;

        public MainMenuLoader(
            [Named("MainMenu")] NodeLoader nodeLoader,
            [Named("MainMenu")] ScriptLoader scriptLoader,
            IEnumerable<IMainMenuButton> menuButtons
        ) {
            _nodeLoader = nodeLoader;
            _scriptLoader = scriptLoader;
            _menuButtons = menuButtons.OrderBy(b => b.Priority);
        }

        public Node Load() {
            var menu = _nodeLoader.Load();
            var script = _scriptLoader.Get();
            if (script != null) {
                menu.SetScript(script);
            }

            foreach (var button in _menuButtons.Select(b => b.GetButton())) {
                if (!menu.HasSignal("MenuButtonRegistration")) {
                    throw new NotImplementedException();
                }
                
                menu.EmitSignal("MenuButtonRegistration", button);
            }

            return menu;
        }
    }
}