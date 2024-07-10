using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [HideInInspector] public bool fallen;
    [SerializeField] private Animator animator;
    [SerializeField] private WanderMovement movement;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Rigidbody rb;

    public void Fall(Vector3 dir)
    {
        fallen = true;
        movement.Stop();
        animator.enabled = false;
        characterController.enabled = false;
        rb.AddForce(dir * 150, ForceMode.Impulse);
        StartCoroutine(DisableRagdoll());
    }

    IEnumerator DisableRagdoll()
    {
        yield return new WaitForSeconds(4);
        Rigidbody[] rest = GetComponentsInChildren<Rigidbody>();
        for (int i = 1; i < rest.Length; i++)
        {
            rest[i].isKinematic = true;
            rest[i].GetComponent<Collider>().enabled = false;
        }
    }

    public void PickUp()
    {
        animator.enabled = true;
        animator.Play("Idle");
        rb.isKinematic = true;
        Collider[] rest = GetComponentsInChildren<Collider>();
        for (int i = 0; i < rest.Length; i++)
        {
            rest[i].enabled = false;
        }
    }
}
