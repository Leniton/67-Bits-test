using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Animator animator;
    [SerializeField] private TriggerDetector punchDetector;

    private Coroutine punchCoroutine;//way to detect if it is already punching

    private void Awake()
    {
        Event<JoystickInputEvent>.OnEvent += OnJoystickInput;
        Event<PunchTriggeredEvemt>.OnEvent += OnPunch;

        punchDetector.onTrigger += OnTrigger;
    }

    private void OnJoystickInput(JoystickInputEvent input)
    {
        Vector3 movementDirection = input.Direction;

        movementDirection.z = movementDirection.y;
        movementDirection.y = 0;
        movement.ChangeDirection(movementDirection);
    }

    private void OnPunch(PunchTriggeredEvemt evt)
    {
        if (punchCoroutine != null) return;
        animator.SetTrigger("Punch");
        StartCoroutine(DelayedPunchTrigger());
    }

    //total time: .58s
    WaitForSeconds delayToActivate = new WaitForSeconds(.28f);
    WaitForSeconds delayToDeactivate = new WaitForSeconds(.3f);
    IEnumerator DelayedPunchTrigger()
    {
        yield return delayToActivate;
        punchDetector.SetActive(true);

        yield return delayToDeactivate;
        punchDetector.SetActive(false);
    }

    private void OnTrigger(Collider other)
    {
        if(other.TryGetComponent<NPC>(out  var npc))
        {
            npc.Fall(transform.forward);
        }
    }
}
