using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvent { }

public static class Event<T> where T : IEvent
{
    public static Action<T> OnEvent;

    public static void CallEvent(T evt) => OnEvent?.Invoke(evt);
}
