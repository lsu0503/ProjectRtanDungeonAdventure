using System;

public class PlayerController : CharacterController
{
    public event Action OnJumpEvent;
    public event Action<bool> OnSprintEvent;
    public event Action OnDashEvent;

    public void CallJumpEvent()
    {
        OnJumpEvent?.Invoke();
    }

    public void CallSprintEvent(bool isOn)
    {
        OnSprintEvent?.Invoke(isOn);
    }

    public void CallDashEvent()
    {
        OnDashEvent?.Invoke();
    }
}
