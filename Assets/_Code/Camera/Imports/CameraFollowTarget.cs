using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour, IFollowTarget
{
    [SerializeField] private Transform targetToFollow;
    [SerializeField] private Vector3 offset;
    // [SerializeField] private Vector3 offset;

    private void Awake()
    {
        if (targetToFollow == null)
        {
            Debug.LogError("The camera: " + this.name + " could not find target to follow. Camera AI disabled.");
            this.enabled = false;
        }
    }

    public void FollowTarget(Transform target)
    {
        Vector3 updatedCameraPos = target.position + this.transform.rotation * offset;
        this.transform.position = updatedCameraPos;
    }

    void LateUpdate()
    {
        FollowTarget(targetToFollow);   
    }
}
