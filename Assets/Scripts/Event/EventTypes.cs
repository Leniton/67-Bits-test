using UnityEngine;

public struct JoystickInputEvent: IEvent
{
    public Vector2 Direction;
    public JoystickInputEvent(Vector2 direction) => Direction = direction;
}

public struct PunchTriggeredEvent: IEvent 
{
    public float duration;
    public PunchTriggeredEvent(float _duration = .2f) => duration = _duration;
}

public struct PlayerMoneyChangedEvent : IEvent
{
    public int newValue;
    public int delta;
    public PlayerMoneyChangedEvent(int newValue, int delta)
    {
        this.newValue = newValue;
        this.delta = delta;
    }
}