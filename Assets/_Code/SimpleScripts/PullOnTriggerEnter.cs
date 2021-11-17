using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PullOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private float _pullSpeed = 10.0f;
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.forward;
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 pullDirection = (this.transform.position + offset - other.transform.position).normalized;

        other.transform.position += pullDirection * _pullSpeed * Time.deltaTime;
    }
}
