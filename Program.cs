using SFML.Graphics;
using SFML.System;
using SFML.Window;
using QWER;
using Components;
using Systems;

var window = new RenderWindow(new VideoMode(new Vector2u(ECSConfig.WindowWidth, ECSConfig.WindowHeight)), "My SFML Window");
window.Closed += (sender, e) => window.Close();
window.SetFramerateLimit(ECSConfig.Framerate);

var c = new Coordinator();

// Static platform, standing under the player.
var platformId = c.CreateEntity();
c.AddComponent(platformId, new PositionComponent { x = 300, y = 400 });
c.AddComponent(platformId, new HitboxComponent { width = 200, height = 32 });

// Player-ish test entity: sits above the platform, has every component
// the movement/jump/gravity systems touch.
var playerId = c.CreateEntity();
c.AddComponent(playerId, new PositionComponent { x = 380, y = 300 });
c.AddComponent(playerId, new VelocityComponent { x = 0, y = 0 });
c.AddComponent(playerId, new HitboxComponent { width = 32, height = 32 });
c.AddComponent(playerId, new ControlComponent { movementDirection = 0, jumpPressed = false, jumpHeldLastFrame = false });
c.AddComponent(playerId, new JumpComponent { initialImpulse = 400, maxJumps = 2, jumpsRemaining = 2 });
c.AddComponent(playerId, new DashComponent
{
    speed = ECSConfig.DefaultDashSpeed,
    duration = ECSConfig.DefaultDashDuration,
    cooldown = ECSConfig.DefaultDashCooldown
});
c.AddComponent(playerId, new GroundedComponent { isGrounded = false });
c.AddComponent(playerId, new GravityComponent { acceleration = 900, terminalVelocity = 700 });
c.AddComponent(playerId, new PlatformerComponent
{
    speedOnGround = 1500,
    terminalVelocityGround = 250,
    speedOnAir = 900,
    terminalVelocityAir = 200
});
c.AddComponent(playerId, new PlayerControlledComponent());

var idleTexture = new Texture("Assets/Samurai/Idle.png");
var walkTexture = new Texture("Assets/Samurai/Walk.png");
var runTexture = new Texture("Assets/Samurai/Run.png");
var jumpTexture = new Texture("Assets/Samurai/Jump.png");

var idleFrameCount = (int)(idleTexture.Size.X / ECSConfig.SamuraiFrameSize);
var walkFrameCount = (int)(walkTexture.Size.X / ECSConfig.SamuraiFrameSize);
var runFrameCount = (int)(runTexture.Size.X / ECSConfig.SamuraiFrameSize);
var jumpFrameCount = (int)(jumpTexture.Size.X / ECSConfig.SamuraiFrameSize);

c.AddComponent(playerId, new SpriteComponent
{
    texture = idleTexture,
    sprite = new Sprite(idleTexture)
});
c.AddComponent(playerId, new AnimationSpriteSheets
{
    idleTexture = idleTexture,
    idleFrameCount = idleFrameCount,
    walkingTexture = walkTexture,
    walkingFrameCount = walkFrameCount,
    runningTexture = runTexture,
    runningFrameCount = runFrameCount,
    jumpingTexture = jumpTexture,
    jumpingFrameCount = jumpFrameCount
});
c.AddComponent(playerId, new AnimationComponent
{
    facingRight = true,
    timer = 0f,
    frameDuration = ECSConfig.AnimationFrameDuration,
    state = Components.AnimationState.Idle,
    frameIndex = 0,
    frameCount = idleFrameCount
});

// Built once, after all static geometry (platforms/walls) exists, and shared
// by every system that needs to test collision against the world.
var geometry = new Physics.StaticGeometry(c);

// Input/Jump/Dash/Gravity/Platformer before MovementSystem, so the frame's
// final velocity is what gets integrated into position ;)
// Dash runs after Platformer so it overrides x-velocity instead of being
// overridden by it, and before Movement so the dash velocity gets integrated.
var sm = new SystemManager(c);
sm.RegisterSystem(new GravitySystem());
sm.RegisterSystem(new InputSystem());
sm.RegisterSystem(new JumpSystem(geometry));
sm.RegisterSystem(new PlatformerSystem());
sm.RegisterSystem(new DashSystem(geometry));
sm.RegisterSystem(new MovementSystem());
sm.RegisterSystem(new AnimationSystem());

var renderSystem = new RenderSystem();

Clock deltaClock = new Clock();
Clock debugClock = new Clock();

while (window.IsOpen)
{
    Time deltaTime = deltaClock.Restart();

    sm.Update(deltaTime.AsSeconds());

    if (debugClock.ElapsedTime.AsSeconds() > ECSConfig.DebugPrintIntervalSeconds)
    {
        var pos = c.GetComponent<PositionComponent>(playerId);
        var vel = c.GetComponent<VelocityComponent>(playerId);
        var grounded = c.GetComponent<GroundedComponent>(playerId);
        Console.WriteLine($"[state] pos=({pos.x:F1},{pos.y:F1}) vel=({vel.x:F1},{vel.y:F1}) grounded={grounded.isGrounded}");
        debugClock.Restart();
    }

    window.DispatchEvents();
    window.Clear(Color.Black);
    renderSystem.Update(c, window);
    window.Display();
}
