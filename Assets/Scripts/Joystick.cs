using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform handle;
    private RectTransform joystick;
    private float radius;
    Vector2 screenScale = Vector2.one;

    private void Awake()
    {
        joystick = transform as RectTransform;
        radius = joystick.sizeDelta.x * .5f;

        //default screen size is 1920x1080
        screenScale.x = 1920f / Screen.width;
        screenScale.y = 1080f / Screen.height;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateHandler(eventData.position * screenScale);
    }

    public void OnDrag(PointerEventData eventData)
    {
        GameObject target = eventData.pointerCurrentRaycast.gameObject;
        if (target != handle.gameObject && target != joystick.gameObject && target != null)
        {
            OnPointerUp(eventData);
            return;
        }

        UpdateHandler(eventData.position * screenScale);
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
