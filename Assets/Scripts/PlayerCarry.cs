using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    [SerializeField] TriggerDetector pickDetector;
    [SerializeField] Carrier carrier;

    private void Awake()
    {
        pickDetector.onTrigger += OnPickupRange;
    }

    private void OnPickupRange(Collider collider)
    {
        if (collider.TryGetComponent<NPC>(out var npc))
        {
            if (npc.fallen)
            {
                carrier.SetCarried(npc.transform);
                npc.PickUp();
            }
        }
    }
}
