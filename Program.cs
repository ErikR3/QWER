using SFML.Graphics;
using SFML.System;
using SFML.Window;
using QWER;
using Components;
using Systems;

var window = new RenderWindow(new VideoMode(new Vector2u(800, 600)), "My SFML Window");
window.Closed += (sender, e) => window.Close();
window.SetFramerateLimit(ECSConfig.Framerate);

var c = new Coordinator();

// --- Test scaffolding used to manually verify Jump/Gravity/Platformer. Commented out. ---
// // Static platform, standing under the player.
// var platformId = c.CreateEntity();
// c.AddComponent(platformId, new PositionComponent { x = 300, y = 400 });
// c.AddComponent(platformId, new HitboxComponent { width = 200, height = 32 });
//
// // Player-ish test entity: sits above the platform, has every component
// // the movement/jump/gravity systems touch.
// var playerId = c.CreateEntity();
// c.AddComponent(playerId, new PositionComponent { x = 380, y = 300 });
// c.AddComponent(playerId, new VelocityComponent { x = 0, y = 0 });
// c.AddComponent(playerId, new HitboxComponent { width = 32, height = 32 });
// c.AddComponent(playerId, new ControlComponent { movementDirection = 0, jumpPressed = false, jumpHeldLastFrame = false });
// c.AddComponent(playerId, new JumpComponent { initialImpulse = 400, maxJumps = 2, jumpsRemaining = 2 });
// c.AddComponent(playerId, new GroundedComponent { isGrounded = false });
// c.AddComponent(playerId, new GravityComponent { acceleration = 900, terminalVelocity = 700 });
// c.AddComponent(playerId, new PlatformerComponent
// {
//     speedOnGround = 1500,
//     terminalVelocityGround = 250,
//     speedOnAir = 900,
//     terminalVelocityAir = 200
// });

// Jump/Gravity/Platformer before MovementSystem, so the frame's final
// velocity is what gets integrated into position ;)
var sm = new SystemManager(c);
// sm.RegisterSystem(new GravitySystem());
// sm.RegisterSystem(new JumpSystem(c));
// sm.RegisterSystem(new PlatformerSystem());
sm.RegisterSystem(new MovementSystem());

Clock deltaClock = new Clock();
// Clock debugClock = new Clock();

while (window.IsOpen)
{
    Time deltaTime = deltaClock.Restart();

    // ref var control = ref c.GetComponent<ControlComponent>(playerId);
    // control.jumpPressed = Keyboard.IsKeyPressed(Keyboard.Key.Space);
    // var left = Keyboard.IsKeyPressed(Keyboard.Key.A) ? -1f : 0f;
    // var right = Keyboard.IsKeyPressed(Keyboard.Key.D) ? 1f : 0f;
    // control.movementDirection = left + right;

    sm.Update(deltaTime.AsSeconds());

    // if (debugClock.ElapsedTime.AsSeconds() > 0.5f)
    // {
    //     var pos = c.GetComponent<PositionComponent>(playerId);
    //     var vel = c.GetComponent<VelocityComponent>(playerId);
    //     var grounded = c.GetComponent<GroundedComponent>(playerId);
    //     Console.WriteLine($"[state] pos=({pos.x:F1},{pos.y:F1}) vel=({vel.x:F1},{vel.y:F1}) grounded={grounded.isGrounded}");
    //     debugClock.Restart();
    // }

    window.DispatchEvents();
    window.Clear(Color.Black);
    window.Display();
}
