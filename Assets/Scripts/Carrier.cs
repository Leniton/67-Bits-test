using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    [SerializeField] private Transform anchorPoint;
    [SerializeField] private Vector3 offset;
    Transform carried;

    public void SetCarried(Transform carried)
    {
        this.carried = carried;
        if (carried == null) return;
        carried.position = anchorPoint.TransformPoint(offset);
    }

    void FixedUpdate()
    {
        if (carried == null) return;
        carried.position = anchorPoint.TransformPoint(offset);
        LerpRotation();
    }

    private void LerpRotation()
    {
        carried.forward = Vector3.Lerp(carried.forward, anchorPoint.TransformDirection(Vector3.forward),.15f);
    }
}
