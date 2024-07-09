using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    public int maxPickUp = 1;
    [SerializeField] TriggerDetector pickDetector;
    [SerializeField] Carrier carrier;

    private List<Carrier> subCarriers = new();

    private void Awake()
    {
        pickDetector.onTrigger += OnPickupRange;
        subCarriers.Add(carrier);
    }

    private void OnPickupRange(Collider collider)
    {
        if (collider.TryGetComponent<NPC>(out var npc))
        {
            if (npc.fallen)
            {
                subCarriers.Add(npc.GetComponent<Carrier>());
                npc.PickUp();
                UpdateCarriers();
            }
        }
    }

    private void UpdateCarriers()
    {
        for (int i = 0; i < subCarriers.Count - 1; i++)
        {
            subCarriers[i].SetCarried(subCarriers[i + 1].transform);
        }
    }
}
