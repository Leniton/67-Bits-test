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
        Event<PunchTriggeredEvent>.OnEvent += OnPunch;

        punchDetector.onTrigger += OnTrigger;
    }

    private void OnJoystickInput(JoystickInputEvent input)
    {
        Vector3 movementDirection = input.Direction;

        movementDirection.z = movementDirection.y;
        movementDirection.y = 0;
        movement.ChangeDirection(movementDirection);
    }

    private void OnPunch(PunchTriggeredEvent evt)
    {
        if (punchCoroutine != null) return;
        animator.SetTrigger("Punch");
        delayToActivate = new WaitForSeconds(evt.duration * .4f);
        delayToDeactivate = new WaitForSeconds(evt.duration * .3f);
        punchCoroutine = StartCoroutine(DelayedPunchTrigger());
    }

    WaitForSeconds delayToActivate;
    WaitForSeconds delayToDeactivate;
    IEnumerator DelayedPunchTrigger()
    {
        yield return delayToActivate;
        punchDetector.SetActive(true);

        yield return delayToDeactivate;
        punchDetector.SetActive(false);
        punchCoroutine = null;
    }

    private void OnTrigger(Collider other)
    {
        if(other.TryGetComponent<NPC>(out  var npc))
        {
            punchDetector.SetActive(false);
            npc.Fall(transform.forward);
        }
    }
}
