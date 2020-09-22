using System.Collections.Generic;
using Godot;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.Loader.Attributes;
using ScientiaMagica.Common.Resources;

namespace ScientiaMagica.Options {
    [DefaultInject(typeof(OptionMenu))]
    public class OptionMenu : CanvasLayer {
        private readonly Dictionary<string, Panel> _categories;
        private readonly TabContainer _tabs;
        
        public OptionMenu(IEnumerable<IOptionEntry> entries) {
            this.InheritSceneStructure("res://ScientiaMagica.Options/src/Scenes/OptionsMenu.tscn");
            
            _categories = new Dictionary<string, Panel>();

            _tabs = GetNode<TabContainer>("OptionsMenu/Categories");
            
            foreach (var entry in entries) {
                var category = GetCategory(entry.Category);
                var container = category.GetNode<VBoxContainer>("EntriesScroller/Entries");
                container.AddChild(entry.GetOptionEntry());
            }
        }

        private Panel GetCategory(string name) {
            if (_categories.TryGetValue(name, out var panel)) {
                return panel;
            }
            
            panel = (Panel)new NodeLoader("res://ScientiaMagica.Options/src/Scenes/OptionsCategory.tscn").Load();
            panel.Name = name;
            
            _categories.Add(name, panel);
            _tabs.AddChild(panel);

            return panel;
        }
    }
}