# Asset Reference Generator

This folder uses the T4 template system  to generate a struct of scene loaders that point
to all scenes in your godot project. You can put this in any subdirectory as long as there
is a `*.godot` file in a parent directory. A struct will be generated that contains a
loader for every scene file.

For example let's say you want to load `res://ScientiaMagica.Game/src/Scenes/Player.tscn`
```C#
using ScientiaMagica.Common.Assets;

public class Foo {
    private readonly Node _player;

    public Foo() {
        _player = Scenes.Player.Instance();
    }
}
```

No magic strings required!

In the future I'll look making the filename have a bit more control over how you reference
it, so you can do something like `Scenes.Characters.Player.Instance()`
by naming your scene file `Characters.Player.tscn`.

If you wish to use this you may want to change the namespace found in the `.tt` file to 
something more appropriate to your project.