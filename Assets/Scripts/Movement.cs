using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 currentDirection = Vector3.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Event<JoystickInputEvent>.OnEvent += OnMovementInput;
    }

    private void OnMovementInput(JoystickInputEvent evt)
    {
        Vector3 movementDirection = evt.Direction;

        movementDirection.z = movementDirection.y;
        movementDirection.y = 0;
        currentDirection = movementDirection;
    }

    private void FixedUpdate()
    {
        controller.Move(currentDirection);
    }
}
