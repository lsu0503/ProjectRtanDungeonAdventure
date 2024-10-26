using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public event Action<Vector2> OnLookEvent;
    public event Action OnInteractionEvent;

    public void CallLookEvent(Vector2 delta)
    {
        OnLookEvent?.Invoke(delta);
    }

    public void CallInteractionEvent()
    {
        OnInteractionEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        CallLookEvent(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            CallInteractionEvent();
        }
    }
}