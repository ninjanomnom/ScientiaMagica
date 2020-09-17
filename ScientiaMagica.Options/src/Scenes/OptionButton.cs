using Godot;
using System;
using JetBrains.Annotations;
using ScientiaMagica.Common.Signals;
using ScientiaMagica.Options;

[UsedImplicitly]
public class OptionButton : OptionButtonImplementation { }

namespace ScientiaMagica.Options {
    public class OptionButtonImplementation : Button {
        public override void _Ready() {
            Connect(ButtonSignals.Pressed, this, nameof(ButtonPressed));
        }

        private void ButtonPressed() {
            throw new NotImplementedException();
        }
    }
}
