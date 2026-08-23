using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Components;

public struct PositionComponent
{
    public float x;
    public float y;
}

public struct VelocityComponent
{
    public float x;
    public float y;
}

public struct ControlComponent
{
    public float movementDirection;
    public bool jumpPressed;
    public bool jumpHeldLastFrame;
}

public struct PlatformerComponent
{
    public bool inContactWithPlatform;
    public float speedOnGround;
    public float speedOnAir;
}

public struct JumpComponent
{
    public float initialImpulse;
    public int maxJumps;
    public int jumpsRemaining;
}

public struct DashComponent
{
    public float speed;
    public bool dashing;
}

public struct SpriteComponent
{
    public Texture texture;
    public Sprite sprite;
}

public struct HitboxComponent
{
    public float width;
    public float height;
}

public struct GravityComponent
{
    public float terminalVelocity;
    public float acceleration;
}
