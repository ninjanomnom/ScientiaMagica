using System;
using Godot;
using ScientiaMagica.Common.GodotExtensions;

namespace ScientiaMagica.Common.GUI {
    public enum MainMenuPriorityOrder {
        OverNewGame,
        AtNewGame,
        OverLoad,
        AtLoad,
        OverExit,
        AtExit,
        UnderExit
    }
}