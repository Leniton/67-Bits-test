using UnityEngine;

public struct JoystickInputEvent: IEvent
{
    public Vector2 Direction;
    public JoystickInputEvent(Vector2 direction) => Direction = direction;
}

public struct PunchButtonEvemt: IEvent { }