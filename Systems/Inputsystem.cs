namespace Systems;

using QWER;
using Components;
using SFML.Window;


public class InputSystem : ISystem
{
    public void Update(Coordinator coordinator, float deltaTime)
    {
        var entityList = coordinator.GetEntitiesWith<ControlComponent, PlayerControlledComponent>();

        foreach (var entityId in entityList)
        {
            ref var entityControl = ref coordinator.GetComponent<ControlComponent>(entityId);
            entityControl.jumpPressed = Keyboard.IsKeyPressed(Keyboard.Key.Space);

            var left = Keyboard.IsKeyPressed(Keyboard.Key.A) ? -1f : 0f;
            var right = Keyboard.IsKeyPressed(Keyboard.Key.D) ? 1f : 0f;
            entityControl.movementDirection = left + right;
        }
    }
}
