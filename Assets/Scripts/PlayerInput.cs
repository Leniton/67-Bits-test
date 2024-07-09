using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] Collider punchDetector;

    private void Awake()
    {
        Event<JoystickInputEvent>.OnEvent += OnJoystickInput;
    }

    private void OnJoystickInput(JoystickInputEvent input)
    {
        Vector3 movementDirection = input.Direction;

        movementDirection.z = movementDirection.y;
        movementDirection.y = 0;
        movement.ChangeDirection(movementDirection);
    }
}
