using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    public int maxPickUp = 1;
    [SerializeField] private TriggerDetector pickDetector;
    [SerializeField] private Carrier carrier;

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

    //
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
        return total;
    }

    private void UpdateCarriers()
    {
        for (int i = 0; i < subCarriers.Count - 1; i++)
        {
            subCarriers[i].SetCarried(subCarriers[i + 1].transform);
        }
        subCarriers[^1].SetCarried(null);
    }
}
