namespace Systems;

using QWER;
using Components;

public class JumpSystem : ISystem
{
    private List<int> validPlatforms;

    public JumpSystem(Coordinator coordinator)
    {
        validPlatforms = new List<int>();
        var platformIteration = coordinator.GetEntitiesWith<HitboxComponent, PositionComponent>();
        foreach (int id in platformIteration)
        {
            if (!coordinator.HasComponent<VelocityComponent>(id))
            {
                validPlatforms.Add(id);
            }
        }
    }

    public void Update(Coordinator coordinator, float deltaTime)
    {
        var movableEntities = coordinator.GetEntitiesWith<JumpComponent, PositionComponent>();

        foreach (int entityId in movableEntities)
        {
            var entityPos = coordinator.GetComponent<PositionComponent>(entityId);

            foreach (int platformId in validPlatforms)
            {
                var platformPos = coordinator.GetComponent<PositionComponent>(platformId);
            }
        }
    }
}
