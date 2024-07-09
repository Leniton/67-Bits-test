using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] RectTransform handle;
    private RectTransform joystick;
    private float radius;

    private void Awake()
    {
        joystick = transform as RectTransform;
        radius = joystick.sizeDelta.x * .5f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateHandler(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateHandler(eventData.position);
    }

    private void UpdateHandler(Vector2 mousePosition)
    {
        //check distance from center of joystick to get direction
        Vector2 direction = mousePosition - joystick.anchoredPosition;

        //clamp handle position to joystick radius
        float distance = direction.magnitude;
        float scale = Mathf.Clamp01(distance / radius);
        direction.Normalize();
        handle.anchoredPosition = (direction * scale) * radius;

        //call event
        Event<JoystickInputEvent>.CallEvent(new(direction));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //center handle
        handle.anchoredPosition = Vector2.zero;
        //call event
        Event<JoystickInputEvent>.CallEvent(new(Vector2.zero));
    }
}
