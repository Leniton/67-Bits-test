using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private CharacterController controller;
    private Vector3 currentDirection = Vector3.zero;

    [Space,SerializeField]private Animator animator;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void ChangeDirection(Vector3 direction)
    {
        currentDirection = direction;
    }

    private void FixedUpdate()
    {
        controller.Move(currentDirection * Time.deltaTime * speed);
        transform.LookAt(transform.position + currentDirection);
        animator.SetFloat("Speed", currentDirection.magnitude);
    }
}
