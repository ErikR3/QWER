namespace Systems;

using QWER;
using Components;

public class MovementSystem : ISystem {
    public MovementSystem()
    {

    }

    public void Update(Coordinator coordinator, float deltaTime)
    {
        // Get all entities with moving components.
        foreach (int entityId in coordinator.GetEntitiesWith<PositionComponent, VelocityComponent>())
        { // Grab all entities and their positions and velocity.
            ref var pos = ref coordinator.GetComponent<PositionComponent>(entityId);
            ref var velocity = ref coordinator.GetComponent<VelocityComponent>(entityId);
            pos.x += velocity.x * deltaTime;
            pos.y += velocity.y * deltaTime;
        }
    }
}
