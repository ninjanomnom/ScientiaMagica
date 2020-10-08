# Asset Reference Generator

This folder uses the T4 template system to generate a struct of scene loaders that point
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

### How to use

Just download this directory and put it somewhere in your project. The csproj file need to
reference `AssetTemplate.tt` and `SceneLoader.cs`. By default Visual Studio and
Jetbrains Rider can build T4 template files and, while I haven't tested, VS Code also has
support and there's an editor agnostic mono version at https://github.com/mono/t4

You're going to want to change the namespace found in the `.tt` file to something more
appropriate to your project.

### Coming soon (maybe)

In the future I'll look making the filename have a bit more control over how you reference
it, so you can do something like `Scenes.Characters.Player.Instance()`
by naming your scene file `Characters.Player.tscn`.