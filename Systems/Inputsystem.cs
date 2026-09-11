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
            entityControl.runHeld = Keyboard.IsKeyPressed(Keyboard.Key.LShift);
            // Shift is dual-purpose: DashSystem only reacts to the press edge
            // (dashPressed && !dashHeldLastFrame), so tapping/holding Shift
            // dashes once, then falls through to running for as long as it's held.
            entityControl.dashPressed = entityControl.runHeld;

            var left = Keyboard.IsKeyPressed(Keyboard.Key.A) ? -1f : 0f;
            var right = Keyboard.IsKeyPressed(Keyboard.Key.D) ? 1f : 0f;
            entityControl.movementDirection = left + right;
        }
    }
}
