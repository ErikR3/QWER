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
            ref var entitySprite = ref coordinator.GetComponent<SpriteComponent>(entityId);
            var entityControl = coordinator.GetComponent<ControlComponent>(entityId);
        }
    }
}
