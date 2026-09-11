namespace Physics;

using QWER;
using Components;

// Shared cache of "static geometry" entities (Hitbox+Position, no Velocity)
// built once and reused by any system that needs to test collision against
// the world (JumpSystem for ground, DashSystem for walls, ...) instead of
// each system separately querying and caching its own copy of the same list.
public class StaticGeometry
{
    public IReadOnlyList<int> Entities => entities;

    private readonly List<int> entities;

    public StaticGeometry(Coordinator coordinator)
    {
        entities = new List<int>();
        var candidates = coordinator.GetEntitiesWith<HitboxComponent, PositionComponent>();
        foreach (int id in candidates)
        {
            if (!coordinator.HasComponent<VelocityComponent>(id))
            {
                entities.Add(id);
            }
        }
    }
}
