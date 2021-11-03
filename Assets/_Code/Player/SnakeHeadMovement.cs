using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float turnSpeedH = 90;
    [SerializeField] private float turnSpeedV = 30;
    [SerializeField] private bool isBoosting;
    
    private Vector3 direction = new Vector3(0,0,1);
    private float currentSpeed = 1.0f;
    
    private float angleH = 0;
    private float angleV = 0;
    Quaternion rotH = Quaternion.identity;
    Quaternion rotV = Quaternion.identity;
    
    private void Update()
    {
        AdjustRotation();
        AdjustPosition();
    }
    
    void AdjustRotation()
    {
        direction = this.transform.rotation * Vector3.forward;
    }

    void AdjustPosition()
    {
        this.transform.position += direction * currentSpeed * Time.deltaTime;
    }

    public void TurnHorizontal(float angleDir)
    {
        angleH += angleDir * turnSpeedH * Time.deltaTime;
        rotH = Quaternion.AngleAxis(angleH, Vector3.up);

        this.transform.rotation = rotV * rotH;
    }
    
    public void TurnVertical(float angleDir)
    {
        angleV += angleDir * turnSpeedV * Time.deltaTime;
        rotV = Quaternion.AngleAxis(angleV, transform.right);

        this.transform.rotation = rotV * rotH;
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
