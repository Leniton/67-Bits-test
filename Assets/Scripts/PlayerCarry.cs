using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    public int maxPickUp = 1;
    [SerializeField] private TriggerDetector pickDetector;
    [SerializeField] private Carrier carrier;
    [SerializeField] private Animator animator;

    private List<Carrier> subCarriers = new();

    private Coroutine coroutine;

    private void Awake()
    {
        Event<PunchTriggeredEvent>.OnEvent += DisableOnPunch;
        pickDetector.onTrigger += OnPickupRange;
        subCarriers.Add(carrier);
    }

    private void OnPickupRange(Collider collider)
    {
        if (subCarriers.Count > maxPickUp) return;
        var npc = collider.GetComponentInParent<NPC>();
        if (npc && npc.fallen)
        {
            Carrier subCarrier = npc.GetComponent<Carrier>();
            if (!subCarriers.Contains(subCarrier))
            {
                subCarriers.Add(subCarrier);
                npc.PickUp();
                UpdateCarriers();
            }
        }
    }

    private void DisableOnPunch(PunchTriggeredEvent evt)
    {
        if (coroutine != null) return;
        pickDetector.onTrigger -= OnPickupRange;
        coroutine = StartCoroutine(EnableAfterPunch(evt.duration));
    }

    private IEnumerator EnableAfterPunch(float duration)
    {
        yield return new WaitForSeconds(duration + .2f);//duration + extra frame
        pickDetector.onTrigger += OnPickupRange;
        coroutine = null;
    }

    public int ClearCarriers()
    {
        int total = subCarriers.Count - 1;
        subCarriers[0].SetCarried(null);
        for (int i = 1; i < total + 1; i++)
        {
            subCarriers[1].SetCarried(null);
            Destroy(subCarriers[1].gameObject);
            subCarriers.RemoveAt(1);
        }
        animator.SetBool("Heavy", false);
        return total;
    }

    private void UpdateCarriers()
    {
        for (int i = 0; i < subCarriers.Count - 1; i++)
        {
            subCarriers[i].SetCarried(subCarriers[i + 1].transform);
        }
        subCarriers[^1].SetCarried(null);
        animator.SetBool("Heavy", subCarriers.Count >= maxPickUp + 1);
    }
}
