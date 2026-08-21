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
    public float velocity_x;
    public float velocity_y;
}

public struct ControlComponent
{
    public float movementDirection;
    public bool jumpPressed;
}

public struct PlatformerComponent
{
    public bool inContactWithPlatform;
    public float speedOnGround;
    public float speedOnAir;
}

public struct JumpComponent
{
    public float speed;
    public bool jumping;
    public float initialImpulse;
}

public struct DashComponent
{
    public float speed;
    public bool dashing;
}

public struct SpriteComponent
{
    // sf::Texture texture;
    // sf::Sprite sprite;
}

public struct HitboxComponent
{
    public float width;
    public float height;
}
