# Asset Reference Generator

This file uses the T4 template system  to generate a struct of path constants that point to 
all scenes in your godot project. You can put this in any subdirectory as long as there is 
a `*.godot` file in a parent directory. A struct will be generated that contains a constant 
for every scene using the file name as the variable name.

For example let's say you want to load `res://ScientiaMagica.Game/src/Scenes/Player.tscn`<br>
```C#
using ScientiaMagica.Common.Assets;

public class Foo {
    private readonly Node _player;

    public Foo() {
        _player = ResourceLoader.Load<PackedScene>(Scenes.Player).Instance();
    }
}
```

No magic strings required!

In the future I'll look making the filename have a bit more control over how you reference
it, so you can do something like `ResourceLoader.Load<PackedScene>(Scenes.Characters.Player)`
by naming your scene file `Characters.Player.tscn`.

If you wish to use this you may want to change the namespace found in the T4 file to 
something more appropriate to your project.