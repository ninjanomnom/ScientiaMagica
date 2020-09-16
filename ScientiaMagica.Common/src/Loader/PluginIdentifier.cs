using System;

namespace ScientiaMagica.Common.Loader {
    public class PluginIdentifier {
        /// <summary>
        /// Used for display purposes
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Must be unique, use something like "ninjanomnom.pluginName.mod"
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// The major version is what gets compared for compatibility
        /// </summary>
        public Version PluginVersion { get; }

        /// <summary>
        /// Makes a new <see cref="PluginIdentifier"/> for locating or labeling a plugin
        /// </summary>
        /// <param name="name"><inheritdoc cref="Name"/></param>
        /// <param name="id"><inheritdoc cref="Id"/></param>
        /// <param name="pluginVersion"><inheritdoc cref="PluginVersion"/></param>
        public PluginIdentifier(string name, string id, Version pluginVersion) {
            Name = name;
            Id = id;
            PluginVersion = pluginVersion;
        }

        public override string ToString() {
            return Name;
        }

        public static bool operator ==(PluginIdentifier a, PluginIdentifier b) {
            return a?.Id == b?.Id;
        }

        public static bool operator !=(PluginIdentifier a, PluginIdentifier b) {
            return a?.Id != b?.Id;
        }

        public override bool Equals(object obj) {
            return obj is PluginIdentifier identifier && (identifier == this);
        }

        public override int GetHashCode() {
            return new {hashtype = GetType(), Id=Id}.GetHashCode();
        }
    }
}