using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnTime : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private Quaternion rotation = Quaternion.identity;
    void FixedUpdate()
    {
        rotation = Quaternion.AngleAxis(Time.deltaTime * _rotationSpeed, Vector3.up);
        this.transform.rotation = rotation * this.transform.rotation;
    }
}
