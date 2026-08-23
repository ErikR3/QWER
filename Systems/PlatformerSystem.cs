namespace Systems;

using QWER;
using Components;

public class PlatformerSystem : ISystem
{
    public void Update(Coordinator coordinator, float deltaTime)
    {
        var platformerEntities = coordinator.GetEntitiesWith<VelocityComponent, PlatformerComponent>();

        foreach (var entityId in platformerEntities)
        {
            ref var entityVel = ref coordinator.GetComponent<VelocityComponent>(entityId);
            ref var entityPlat = ref coordinator.GetComponent<PlatformerComponent>(entityId);
            var entityControl = coordinator.GetComponent<ControlComponent>(entityId);
            var entityGrounded = coordinator.GetComponent<GroundedComponent>(entityId);

            float target;
            float speed;

            if (entityGrounded.isGrounded)
            {
                target = entityControl.movementDirection * entityPlat.terminalVelocityGround;
                speed = entityPlat.speedOnGround;
            }
            else
            {
                target = entityControl.movementDirection * entityPlat.terminalVelocityAir;
                speed = entityPlat.speedOnAir;
            }

            if (entityVel.x < target)
            {
                entityVel.x = Math.Min(entityVel.x + speed * deltaTime, target);
            } else if (entityVel.x > target)
            {
                entityVel.x = Math.Max(entityVel.x - speed * deltaTime, target);
            }
        }
    }
}
