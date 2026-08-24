namespace Systems;

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using QWER;
using Components;

public class RenderSystem {
    public void Update(Coordinator coordinator, RenderWindow window)
    {
        var entities = coordinator.GetEntitiesWith<PositionComponent, HitboxComponent>();

        foreach (var entityId in entities)
        {
            if (!coordinator.HasComponent<SpriteComponent>(entityId))
            {
                continue;
            }

            ref var entitySprite = ref coordinator.GetComponent<SpriteComponent>(entityId);
            ref var entityPos = ref coordinator.GetComponent<PositionComponent>(entityId);

            entitySprite.sprite.Position = new Vector2f(entityPos.x, entityPos.y);
            window.Draw(entitySprite.sprite);
        }
    }
}
