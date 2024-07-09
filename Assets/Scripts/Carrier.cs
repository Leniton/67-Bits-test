using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    [SerializeField] Transform anchorPoint;
    [SerializeField] Vector3 offset;

    [SerializeField] Transform carried;//test only

    private Vector3 lastPosition;
    private Vector3 lastRotation;

    private void Awake()
    {
        //carried.SetParent(anchorPoint);

        lastPosition = carried.position;
        lastRotation = carried.eulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        carried.position = anchorPoint.TransformPoint(offset);

        LerpRotation();

        lastPosition = carried.position;
        lastRotation = carried.eulerAngles;
    }

    private void LerpRotation()
    {
        carried.forward = Vector3.Lerp(carried.forward, anchorPoint.TransformDirection(Vector3.forward),.2f);
    }
}
