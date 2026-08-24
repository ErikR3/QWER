namespace Systems;

using QWER;
using Components;
using SFML.Graphics;
using SFML.System;

public class AnimationSystem : ISystem
{
    private const float DirectionThreshold = 0.01f;

    public void Update(Coordinator coordinator, float deltaTime)
    {
        foreach (var entityId in coordinator.GetEntitiesWith<AnimationComponent, SpriteComponent>())
        {
            ref var anim = ref coordinator.GetComponent<AnimationComponent>(entityId);
            ref var sprite = ref coordinator.GetComponent<SpriteComponent>(entityId);

            float direction = 0f;
            bool runHeld = false;

            if (coordinator.HasComponent<ControlComponent>(entityId))
            {
                ref var control = ref coordinator.GetComponent<ControlComponent>(entityId);
                direction = control.movementDirection;
                runHeld = control.runHeld;
            }
            else if (coordinator.HasComponent<VelocityComponent>(entityId))
            {
                ref var velocity = ref coordinator.GetComponent<VelocityComponent>(entityId);
                direction = velocity.x;
            }

            if (direction > DirectionThreshold)
            {
                anim.facingRight = true;
            }
            else if (direction < -DirectionThreshold)
            {
                anim.facingRight = false;
            }

            var grounded = true;
            if (coordinator.HasComponent<GroundedComponent>(entityId))
            {
                ref var groundedComponent = ref coordinator.GetComponent<GroundedComponent>(entityId);
                grounded = groundedComponent.isGrounded;
            }

            var newState = grounded ? DetermineState(direction, runHeld) : AnimationState.Jumping;
            if (newState != anim.state)
            {
                anim.state = newState;
                anim.frameIndex = 0;
                anim.timer = 0f;

                if (coordinator.HasComponent<AnimationSpriteSheets>(entityId))
                {
                    ref var sheets = ref coordinator.GetComponent<AnimationSpriteSheets>(entityId);
                    ApplyStateTexture(newState, sheets, ref anim, ref sprite);
                }
            }

            anim.timer += deltaTime;
            while (anim.frameDuration > 0f && anim.timer >= anim.frameDuration)
            {
                anim.timer -= anim.frameDuration;
                anim.frameIndex = (anim.frameIndex + 1) % anim.frameCount;
            }

            ApplyFrame(ref anim, ref sprite);
        }
    }

    private static AnimationState DetermineState(float direction, bool runHeld)
    {
        if (MathF.Abs(direction) < DirectionThreshold)
        {
            return AnimationState.Idle;
        }

        return runHeld ? AnimationState.Running : AnimationState.Walking;
    }

    private static void ApplyStateTexture(AnimationState state, AnimationSpriteSheets sheets, ref AnimationComponent anim, ref SpriteComponent sprite)
    {
        var (texture, frameCount) = state switch
        {
            AnimationState.Walking => (sheets.walkingTexture, sheets.walkingFrameCount),
            AnimationState.Running => (sheets.runningTexture, sheets.runningFrameCount),
            AnimationState.Jumping => (sheets.jumpingTexture, sheets.jumpingFrameCount),
            _ => (sheets.idleTexture, sheets.idleFrameCount),
        };

        sprite.texture = texture;
        sprite.sprite.Texture = texture;
        anim.frameCount = frameCount;
    }

    private static void ApplyFrame(ref AnimationComponent anim, ref SpriteComponent sprite)
    {
        if (sprite.texture == null || anim.frameCount <= 0)
        {
            return;
        }

        int frameWidth = (int)(sprite.texture.Size.X / (uint)anim.frameCount);
        int frameHeight = (int)sprite.texture.Size.Y;

        int left = anim.facingRight
            ? anim.frameIndex * frameWidth
            : (anim.frameIndex + 1) * frameWidth;
        int width = anim.facingRight ? frameWidth : -frameWidth;

        sprite.sprite.TextureRect = new IntRect(new Vector2i(left, 0), new Vector2i(width, frameHeight));
    }
}
