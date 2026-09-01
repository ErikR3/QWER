namespace Systems;

using QWER;
using Components;
using Physics;

public class HealthSystem : ISystem
{
    public void Update(Coordinator coordinator, float deltaTime)
    {
        var healthyEntities = coordinator.GetEntitiesWith<HealthComponent, HitboxComponent>();
        var dmgEntities = coordinator.GetEntitiesWith<DamageComponent>();

        foreach (var entityId in healthyEntities)
        {
            var entityHitbox = coordinator.GetComponent<HitboxComponent>(entityId);
            var entityPosition = coordinator.GetComponent<PositionComponent>(entityId);
            ref var entityHealth = ref coordinator.GetComponent<HealthComponent>(entityId);
            foreach (var dmgId in dmgEntities)
            {
                var dmgHitbox = coordinator.GetComponent<HitboxComponent>(dmgId);
                var dmgPosition = coordinator.GetComponent<PositionComponent>(dmgId);
                var dmgDamage = coordinator.GetComponent<DamageComponent>(dmgId);
                if (Collision.OverlapsOnXAxis(entityPosition, entityHitbox, dmgPosition, dmgHitbox) && Collision.OverlapsOnYAxis(entityPosition, entityHitbox, dmgPosition, dmgHitbox))
                {
                    entityHealth.currentHealth -= dmgDamage.hpDamage;
                }
            }
        }
    }
}
