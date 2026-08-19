using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Components;

public struct PositionComponent
{
    float x;
    float y;
}

public struct VelocityComponent
{
    float velocity_x;
    float velocity_y;
}

public struct ControlComponent
{
    float movementDirection;
    bool jumpPressed;
}

struct PlatformerComponent
{
    bool inContactWithPlatform;
    float speedOnGround;
    float speedOnAir;
}

public struct JumpComponent
{
    float speed;
    bool jumping;
    float initialImpulse;
}

public struct DashComponent
{
    float speed;
    bool dashing;
}

public struct SpriteComponent
{
    // sf::Texture texture;
    // sf::Sprite sprite;
}
