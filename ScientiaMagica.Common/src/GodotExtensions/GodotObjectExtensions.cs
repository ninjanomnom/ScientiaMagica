using System.Collections.Generic;
using Godot;
using Godot.Collections;
using JetBrains.Annotations;

namespace ScientiaMagica.Common.GodotExtensions {
    public static class GodotObjectExtensions {
        [PublicAPI]
        public static bool Is<T>(this Object src) where T : Object {
            return src.IsClass(typeof(T).Name);
        }
        
        public class ObjectCopyIntermediate<T> where T : Object {
            private readonly T _copySource;

            private static readonly List<string> IgnoredProperties = new List<string>() {
                "__meta__",
                "script"
            };
            
            internal ObjectCopyIntermediate(T node) {
                _copySource = node;
            }

            [PublicAPI]
            public T To(T node) {
                foreach (Dictionary property in _copySource.GetPropertyList()) {
                    var name = (string)property["name"];
                    if (IgnoredProperties.Contains(name)) {
                        continue;
                    }
                    var got = _copySource.Get(name);
                    node.Set(name, got);
                }

                return node;
            }
        }

        [PublicAPI]
        public static ObjectCopyIntermediate<T> CopyProperties<T>(this T src) where T : Object {
            return new ObjectCopyIntermediate<T>(src);
        }
    }
}