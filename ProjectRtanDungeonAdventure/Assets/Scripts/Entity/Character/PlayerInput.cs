using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : PlayerController
{
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            CallMoveEvent(context.ReadValue<Vector2>());

        else if (context.phase == InputActionPhase.Canceled)
            CallMoveEvent(Vector2.zero);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            CallJumpEvent();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            CallSprintEvent(true);

        else if (context.phase == InputActionPhase.Canceled)
            CallSprintEvent(false);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
            CallDashEvent();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            CallOnAttackEvent();
    }
}