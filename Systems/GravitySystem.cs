namespace Systems;

using QWER;
using Components;

public class GravitySystem : ISystem
{
    public void Update(Coordinator coordinator, float deltaTime)
    {
        var gravitableEntities = coordinator.GetEntitiesWith<VelocityComponent, GravityComponent>();

        foreach (var entityId in gravitableEntities)
        {
            ref var entityVel = ref coordinator.GetComponent<VelocityComponent>(entityId);
            ref var entityGrav = ref coordinator.GetComponent<GravityComponent>(entityId);

            entityVel.y += entityGrav.acceleration * deltaTime;
            entityVel.y = Math.Min(entityVel.y, entityGrav.terminalVelocity);
        }
    }
}
