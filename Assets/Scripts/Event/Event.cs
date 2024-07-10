using System;

public interface IEvent { }

public static class Event<T> where T : IEvent
{
    public static Action<T> OnEvent;

    public static void CallEvent(T evt) => OnEvent?.Invoke(evt);
}
