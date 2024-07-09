using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerDetector : MonoBehaviour
{
    public Action<Collider> onTrigger,onExit;
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    public void SetActive(bool active) => collider.enabled = active;

    private void OnTriggerEnter(Collider other)
    {
        onTrigger?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        onExit?.Invoke(other);
    }
}
