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

    // X-axis mirror of NearGround: which side to test depends on travel direction,
    // since a wall can be approached from the left or the right.
    public static bool NearWall(PositionComponent entityPos, HitboxComponent entityHitbox, VelocityComponent entityVel, PositionComponent wallPos, HitboxComponent wallHitbox, float deltaTime)
    {
        var minTolerance = entityHitbox.width * 0.01f;
        var tolerance = Math.Max(minTolerance, Math.Abs(entityVel.x * deltaTime));

        if (entityVel.x > 0)
        {
            var entityRight = entityPos.x + entityHitbox.width;
            var wallLeft = wallPos.x;
            return Math.Abs(entityRight - wallLeft) < tolerance;
        }
        if (entityVel.x < 0)
        {
            var entityLeft = entityPos.x;
            var wallRight = wallPos.x + wallHitbox.width;
            return Math.Abs(entityLeft - wallRight) < tolerance;
        }
        return false;
    }

    public static bool HitsWall(PositionComponent entityPos, HitboxComponent entityHitbox, VelocityComponent entityVel, PositionComponent wallPos, HitboxComponent wallHitbox, float deltaTime)
    {
        return OverlapsOnYAxis(entityPos, entityHitbox, wallPos, wallHitbox)
            && NearWall(entityPos, entityHitbox, entityVel, wallPos, wallHitbox, deltaTime);
    }
}
