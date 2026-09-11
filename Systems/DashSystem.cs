namespace Systems;

using QWER;
using Components;
using Physics;

public class DashSystem : ISystem
{
    private readonly StaticGeometry geometry;

    public DashSystem(StaticGeometry geometry)
    {
        this.geometry = geometry;
    }

    public void Update(Coordinator coordinator, float deltaTime)
    {
        var dashEntities = coordinator.GetEntitiesWith<DashComponent, VelocityComponent>();

        foreach (int entityId in dashEntities)
        {
            ref var entityDash = ref coordinator.GetComponent<DashComponent>(entityId);
            ref var entityVel = ref coordinator.GetComponent<VelocityComponent>(entityId);
            ref var entityControl = ref coordinator.GetComponent<ControlComponent>(entityId);
            var entityPos = coordinator.GetComponent<PositionComponent>(entityId);
            var entityHitbox = coordinator.GetComponent<HitboxComponent>(entityId);

            if (entityDash.cooldownRemaining > 0)
            {
                entityDash.cooldownRemaining = Math.Max(0, entityDash.cooldownRemaining - deltaTime);
            }

            if (entityDash.dashing)
            {
                entityVel.x = entityDash.direction * entityDash.speed;

                var hitWall = false;
                foreach (int wallId in geometry.Entities)
                {
                    var wallPos = coordinator.GetComponent<PositionComponent>(wallId);
                    var wallHitbox = coordinator.GetComponent<HitboxComponent>(wallId);
                    if (Collision.HitsWall(entityPos, entityHitbox, entityVel, wallPos, wallHitbox, deltaTime))
                    {
                        hitWall = true;
                        break;
                    }
                }

                entityDash.dashTimeRemaining -= deltaTime;

                if (hitWall || entityDash.dashTimeRemaining <= 0)
                {
                    entityDash.dashing = false;
                    entityDash.cooldownRemaining = entityDash.cooldown;
                    if (hitWall)
                    {
                        entityVel.x = 0;
                    }
                }
            }
            else if (DashTriggered(entityControl) && entityDash.cooldownRemaining <= 0)
            {
                entityDash.direction = DashDirection(entityControl, entityVel);
                entityDash.dashing = true;
                entityDash.dashTimeRemaining = entityDash.duration;
            }

            entityControl.dashHeldLastFrame = entityControl.dashPressed;
        }
    }

    private static bool DashTriggered(ControlComponent control)
    {
        return control.dashPressed && !control.dashHeldLastFrame;
    }

    private static float DashDirection(ControlComponent control, VelocityComponent vel)
    {
        if (control.movementDirection != 0)
        {
            return Math.Sign(control.movementDirection);
        }
        if (vel.x != 0)
        {
            return Math.Sign(vel.x);
        }
        return 1f;
    }
}
