using System.Collections.Generic;
using System.Dynamic;
using Godot;
using JetBrains.Annotations;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.Loader.Attributes;

namespace ScientiaMagica.Options {
    [PublicAPI]
    public interface IOptionEntry {
        string Category { get; }
        Node GetOptionEntry();
    }

    [PublicAPI]
    public abstract class DefaultOptionEntry : IOptionEntry {
        public virtual string Category { get; } = "Misc";
        protected abstract string Name { get; }
        
        public virtual Node GetOptionEntry() {
            var grid = new GridContainer {Columns = 2};
            var label = new Label {
                Text = Name,
                SizeFlagsHorizontal = (int) Control.SizeFlags.Expand
            };


            grid.AddChild(label);
            return grid;
        }
    }

    [PublicAPI]
    public abstract class DropdownOptionEntry : DefaultOptionEntry {
        protected abstract IEnumerable<string> Entries { get; }

        public override Node GetOptionEntry() {
            var grid = base.GetOptionEntry();
            var dropdown = new OptionButton {
                SizeFlagsHorizontal = (int) Control.SizeFlags.Expand
            };
            grid.AddChild(dropdown);
            foreach (var entry in Entries) {
                dropdown.AddItem(entry);
            }

            return grid;
        }
    }
}