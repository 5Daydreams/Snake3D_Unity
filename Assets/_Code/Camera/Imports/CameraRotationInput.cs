using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRotationInput : MonoBehaviour
{
    [SerializeField] private Vector2 rotationSpeed = Vector2.one;
    private Vector2 initialRot;
    private Vector2 rotVector;
    private Vector3 initialMousePosition;

    public void MouseDrag(Vector3 mousePosition)
    {
        if (!Input.GetMouseButton(1))
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            initialRot.x = this.transform.rotation.eulerAngles.y;
            initialRot.y = this.transform.rotation.eulerAngles.x;
            initialMousePosition = mousePosition;
        }
        
        Vector3 mouseDelta = mousePosition - initialMousePosition;

        // if (mouseDelta.sqrMagnitude <= 1.5f)
        // {
        //     return;
        // }

        rotVector.x = (initialRot.x + mouseDelta.x) * rotationSpeed.x;
        rotVector.y = (initialRot.y + mouseDelta.y) * rotationSpeed.y;
        
        rotVector.y = Mathf.Clamp(rotVector.y,-65,65);

        Quaternion rotationH = Quaternion.AngleAxis(rotVector.x, Vector3.up);
        Quaternion rotationV = Quaternion.AngleAxis(rotVector.y, Vector3.right);

        this.transform.rotation = rotationH * rotationV;
    }
    
    private void Update()
    {
        MouseDrag(Input.mousePosition);
    }
}
