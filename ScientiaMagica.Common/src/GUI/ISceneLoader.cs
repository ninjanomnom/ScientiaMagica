using Godot;
using JetBrains.Annotations;

namespace ScientiaMagica.Common.GUI {
    public interface ISceneLoader {
        [NotNull]
        Node Load();
    }
}