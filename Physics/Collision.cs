using Components;

namespace Physics;

public static class Collision
{
    public static bool OverlapsOnXAxis(PositionComponent posA, HitboxComponent hitboxA, PositionComponent posB, HitboxComponent hitboxB)
    {
        var aLeft = posA.x;
        var aRight = posA.x + hitboxA.width;
        var bLeft = posB.x;
        var bRight = posB.x + hitboxB.width;

        return aLeft < bRight && aRight > bLeft;
    }

    public static bool OverlapsOnYAxis(PositionComponent posA, HitboxComponent hitboxA, PositionComponent posB, HitboxComponent hitboxB)
    {
        var aTop = posA.y;
        var aBottom = posA.y + hitboxA.height;
        var bTop = posB.y;
        var bBottom = posB.y + hitboxB.height;

        return aTop < bBottom && aBottom > bTop;
    }

    public static bool NearGround(PositionComponent entityPos, HitboxComponent entityHitbox, VelocityComponent entityVel, PositionComponent platformPos, HitboxComponent platformHitbox, float deltaTime)
    {
        var entityBottom = entityPos.y + entityHitbox.height;
        var platformTop = platformPos.y;
        var minTolerance = entityHitbox.height * 0.01f;
        var tolerance = Math.Max(minTolerance, Math.Abs(entityVel.y * deltaTime));

        return Math.Abs(entityBottom - platformTop) < tolerance;
    }

    public static bool IsGrounded(PositionComponent entityPos, HitboxComponent entityHitbox, VelocityComponent entityVel, PositionComponent platformPos, HitboxComponent platformHitbox, float deltaTime)
    {
        return OverlapsOnXAxis(entityPos, entityHitbox, platformPos, platformHitbox)
            && NearGround(entityPos, entityHitbox, entityVel, platformPos, platformHitbox, deltaTime);
    }
}
