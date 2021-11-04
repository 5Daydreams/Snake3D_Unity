using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 basePositionOffset;
    [SerializeField] private Vector3 baseLookOffset;
    private Vector3 lookOffset;
    private Vector3 positionOffset;

    void LateUpdate()
    {
        AdjustRotation();
        AdjustPositioning();
    }

    private void AdjustPositioning()
    {
        this.transform.position = target.position + positionOffset;

        Vector3 direction = target.position + lookOffset - this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void AdjustRotation()
    {
        float angle = target.transform.rotation.eulerAngles.y;
        positionOffset = Quaternion.AngleAxis(angle, Vector3.up) * basePositionOffset;
        lookOffset = Quaternion.AngleAxis(angle, Vector3.up) * baseLookOffset;
    }
}