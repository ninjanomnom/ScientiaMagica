using System;
using System.Collections.Generic;
using Godot;
using ScientiaMagica.Common.GodotExtensions;
using ScientiaMagica.Common.Loader.Attributes;
using ScientiaMagica.Common.Resources;
using ScientiaMagica.Common.Signals;

namespace ScientiaMagica.Options {
    [DefaultInject(typeof(OptionMenu))]
    public class OptionMenu : CanvasLayer {
        private readonly Dictionary<string, Panel> _categories;
        private readonly TabContainer _tabs;
        
        public OptionMenu(IEnumerable<IOptionEntry> entries) {
            this.InheritSceneStructure(Common.Assets.Scenes.OptionsMenu.Instance());
            
            _categories = new Dictionary<string, Panel>();

            _tabs = GetNode<TabContainer>("OptionsMenu/Categories");
            
            foreach (var entry in entries) {
                var category = GetCategory(entry.Category);
                var container = category.GetNode<VBoxContainer>("EntriesScroller/Entries");
                container.AddChild(entry.GetOptionEntry());
            }

            var controls = GetNode<GridContainer>("OptionsMenu/ControlButtons");
            controls.GetNode<Button>("CancelButton").Connect(ButtonSignals.Pressed, this, nameof(CancelButtonPressed));
            controls.GetNode<Button>("ResetButton").Connect(ButtonSignals.Pressed, this, nameof(ResetButtonPressed));
            controls.GetNode<Button>("FinishButton").Connect(ButtonSignals.Pressed, this, nameof(FinishButtonPressed));
            controls.GetNode<Button>("ApplyButton").Connect(ButtonSignals.Pressed, this, nameof(ApplyButtonPressed));
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

        private void CancelButtonPressed() {
            GetParent().RemoveChild(this);
        }

        private void ResetButtonPressed() {
            throw new NotImplementedException();
        }
        
        private void FinishButtonPressed() {
            throw new NotImplementedException();
        }

        private void ApplyButtonPressed() {
            throw new NotImplementedException();
        }
    }
}