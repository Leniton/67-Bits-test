using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [HideInInspector] public bool fallen;
    [SerializeField] Animator animator;
    [SerializeField] WanderMovement movement;

    public void Fall()
    {
        fallen = true;
        movement.Stop();
        animator.SetTrigger("Fall");
    }
}
