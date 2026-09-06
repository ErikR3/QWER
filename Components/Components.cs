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
    public bool runHeld;
}

public struct PlatformerComponent
{
    public float speedOnGround;
    public float terminalVelocityGround;
    public float speedOnAir;
    public float terminalVelocityAir;
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

public struct GroundedComponent
{
    public bool isGrounded;
}

public struct PlayerControlledComponent
{
}

public enum AnimationState
{
    Idle,
    Running,
    Walking,
    Jumping,
}

public struct AnimationComponent
{
    public bool facingRight;
    public float timer;
    public float frameDuration;
    public AnimationState state;
    public int frameIndex;
    public int frameCount;
}

public struct AnimationSpriteSheets
{
    public Texture idleTexture;
    public int idleFrameCount;
    public Texture walkingTexture;
    public int walkingFrameCount;
    public Texture runningTexture;
    public int runningFrameCount;
    public Texture jumpingTexture;
    public int jumpingFrameCount;
}

public struct HealthComponent
{
    public int maxHealth;
    public int currentHealth;
    public float iFrames;
    public bool isDead;
    public float stunFrames;
}

public struct DamageComponent
{
    public int hpDamage;
    public int duration;
}
