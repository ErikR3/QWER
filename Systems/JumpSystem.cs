namespace Systems;

using QWER;
using Components;
using Physics;

public class JumpSystem : ISystem
{
    private readonly StaticGeometry geometry;

    public JumpSystem(StaticGeometry geometry)
    {
        this.geometry = geometry;
    }

    public void Update(Coordinator coordinator, float deltaTime)
    {
        var movableEntities = coordinator.GetEntitiesWith<JumpComponent, PositionComponent>();

        foreach (int entityId in movableEntities)
        {
            ref var entityPos = ref coordinator.GetComponent<PositionComponent>(entityId);
            var entityHitbox = coordinator.GetComponent<HitboxComponent>(entityId);
            ref var entityVel = ref coordinator.GetComponent<VelocityComponent>(entityId);
            ref var entityJump = ref coordinator.GetComponent<JumpComponent>(entityId);
            ref var entityControl = ref coordinator.GetComponent<ControlComponent>(entityId);
            ref var entityGrounded = ref coordinator.GetComponent<GroundedComponent>(entityId);

            var wasGrounded = entityGrounded.isGrounded;
            entityGrounded.isGrounded = false;

            foreach (int platformId in geometry.Entities)
            {
                var platformPos = coordinator.GetComponent<PositionComponent>(platformId);
                var platformHitbox = coordinator.GetComponent<HitboxComponent>(platformId);
                var onPlatform = Collision.IsGrounded(entityPos, entityHitbox, entityVel, platformPos, platformHitbox, deltaTime);
                if (onPlatform)
                {
                    entityGrounded.isGrounded = true;
                    entityJump.jumpsRemaining = entityJump.maxJumps;
                    entityPos.y = platformPos.y - entityHitbox.height;
                    entityVel.y = Math.Min(entityVel.y, 0);
                    break;
                }
            }

            if (entityGrounded.isGrounded != wasGrounded)
            {
                Console.WriteLine($"[JumpSystem] entity {entityId} grounded -> {entityGrounded.isGrounded} (pos.y={entityPos.y:F1}, jumpsRemaining={entityJump.jumpsRemaining})");
            }

            if (JumpTriggered(entityControl))
            {
                if (entityJump.jumpsRemaining > 0)
                {
                    entityVel.y = - entityJump.initialImpulse;
                    entityJump.jumpsRemaining -= 1;
                    Console.WriteLine($"[JumpSystem] entity {entityId} jumped (vel.y={entityVel.y:F1}, jumpsRemaining={entityJump.jumpsRemaining})");
                }
            } else if (JumpReleased(entityControl) && entityVel.y < 0)
            {
                entityVel.y *= 0.3f;
                Console.WriteLine($"[JumpSystem] entity {entityId} jump cut (vel.y={entityVel.y:F1})");
            }
            entityControl.jumpHeldLastFrame = entityControl.jumpPressed;
        }
    }

    public bool JumpTriggered(ControlComponent controlComponent)
    {
        if (controlComponent.jumpPressed && !controlComponent.jumpHeldLastFrame)
        {
            return true;
        }
        return false;
    }

    public bool JumpReleased(ControlComponent controlComponent)
    {
        if (!controlComponent.jumpPressed && controlComponent.jumpHeldLastFrame)
        {
            return true;
        }
        return false;
    }
}
