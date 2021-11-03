using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private bool isBoosting;
    private float currentSpeed = 1.0f;
    private Vector3 direction = new Vector3(0,0,1);
    
    private void Update()
    {
        AdjustRotation();
        AdjustPosition();
    }
    
    void AdjustRotation()
    {
        this.transform.rotation = Quaternion.AngleAxis(Time.deltaTime, Vector3.up) * this.transform.rotation;
        direction = this.transform.rotation * Vector3.forward;
    }

    void AdjustPosition()
    {
        this.transform.position += direction * currentSpeed * Time.deltaTime;
    }

    public void TurnHorizontal(float angle)
    {
        Quaternion rotH = Quaternion.AngleAxis(angle, Vector3.up);
    }

    public void SetSpeedBoost(bool value)
    {
        isBoosting = value;
        
        if (isBoosting)
        {
            currentSpeed = baseSpeed + boostSpeed;
        }
        else
        {
            currentSpeed = baseSpeed;
        }
    }
    
}
