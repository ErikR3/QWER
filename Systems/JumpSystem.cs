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
            var entityHitbox = coordinator.GetComponent<HitboxComponent>(entityId);
            var entityVel = coordinator.GetComponent<VelocityComponent>(entityId);


            foreach (int platformId in validPlatforms)
            {
                var platformPos = coordinator.GetComponent<PositionComponent>(platformId);
                var platformHitbox = coordinator.GetComponent<HitboxComponent>(platformId);
                var onPlatform = IsGrounded(entityPos, entityHitbox, entityVel, platformPos, platformHitbox, deltaTime);
                if (onPlatform)
                {
                    ref var canJump = ref coordinator.GetComponent<JumpComponent>(entityId);
                    canJump.jumpsRemaining = 1;
                }
            }
        }
    }

    public bool IsGrounded(PositionComponent entityPos, HitboxComponent entityHitbox, VelocityComponent entityVel, PositionComponent platformPos, HitboxComponent platformHitbox, float deltaTime)
    {
        if (OverlapsOnXAxis(entityPos, entityHitbox, platformPos, platformHitbox) && NearGround(entityPos, entityHitbox, entityVel, platformPos, platformHitbox, deltaTime))
        {
            return true;
        }

        return false;
    }

    public bool OverlapsOnXAxis(PositionComponent entityPos, HitboxComponent entityHitbox, PositionComponent platformPos, HitboxComponent platformHitbox)
    {
        var entityLeft = entityPos.x;
        var entityRight = entityPos.x + entityHitbox.width;
        var platformLeft = platformPos.x;
        var platformRight = platformPos.x + platformHitbox.width;
        if (entityLeft < platformRight && entityRight > platformLeft)
        {
            return true;
        }
        return false;
    }

    public bool OverlapsOnYAxis(PositionComponent entityPos, HitboxComponent entityHitbox, PositionComponent platformPos, HitboxComponent platformHitbox)
    {
        var entityTop = entityPos.y;
        var entityBottom = entityPos.y + entityHitbox.height;
        var platformTop = platformPos.y;
        var platformBottom = platformPos.y + platformHitbox.height;

        if (entityTop < platformBottom && entityBottom > platformTop)
        {
            return true;
        }

        return false;
    }

    public bool NearGround(PositionComponent entityPos, HitboxComponent entityHitbox, VelocityComponent entityVel, PositionComponent platformPos, HitboxComponent platformHitbox, float deltaTime)
    {
        var entityTop = entityPos.y;
        var entityBottom = entityPos.y + entityHitbox.height;
        var platformTop = platformPos.y;
        var platformBottom = platformPos.y + platformHitbox.height;
        var groundTolerance = entityVel.y * deltaTime;
        var minTolerance = entityHitbox.height * 0.01f;
        var tolerance = Math.Max(minTolerance, Math.Abs(groundTolerance));

        return Math.Abs(entityBottom - platformTop) < tolerance;
    }
}
