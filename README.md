Helluuuu

# QWER

A hand-rolled ECS platformer in C#/SFML.


https://github.com/user-attachments/assets/5a1c8bee-8725-4fd0-a122-9ee565254a93


## Known bugs, disclosed by the project's own docs

- `HealthSystem.cs` only applies damage while `iFrames > 0`, and nothing
   ever raises `iFrames` back up after a hit. That is invincibility frames
   running backwards: you take damage exactly when you're supposed to be
   immune, and are permanently vulnerable once the timer hits zero.

- Mystery files: `Assets/Me.png`. Sitting next to the samurai sprite sheet
  and the dungeon tileset is, apparently, a picture of you. No comment
  in the code explains why. The player character is a samurai. You are
  not a samurai. This is unresolved.


## Setup

\`\`\`
dotnet build
dotnet run
\`\`\`

Or don't. There's 39 MB of prebuilt binaries in `bin/` already committed.
Someone already ran it once. Onto the next platform's DLL folder.
