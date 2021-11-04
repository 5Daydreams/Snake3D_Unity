using System;
using System.Collections;
using System.Collections.Generic;
using _Code.Player;
using UnityEngine;

public class SnakeHeadMovement : MonoBehaviour, ISnakeNode
{
    [Header("Child Node")] 
    [SerializeField] private SnakeBody _firstNode;
    
    [Header("Speed")]
    [SerializeField] private float baseSpeed;
    [SerializeField] private float boostSpeed;

    [Header("Rotation")]
    [SerializeField] private float turnSpeedH = 90;
    [SerializeField] private float turnSpeedV = 50;
    [SerializeField] private float verticalPivotThreshold = 65.0f;

    private Vector3 direction = new Vector3(0,0,1);
    private float currentSpeed = 1.0f;
    private bool isBoosting;

    private float angleH = 0;
    private float angleV = 0;
    Quaternion rotH = Quaternion.identity;
    Quaternion rotV = Quaternion.identity;

    private float timer = 0.0f;
    private float delayBetweenWaypointDrops = 0.0f;

    public ISnakeNode NextNode
    {
        get => _firstNode;
        set => _firstNode = value as SnakeBody;
    }

    public Queue<Vector3> WaypointList { get; set; }

    public ISnakeNode GetLastNode()
    {
        if (NextNode == null)
        {
            return this;
        }

        return NextNode.GetLastNode();
    }

    private void Awake()
    {
        WaypointList = new Queue<Vector3>();
        NextNode.WaypointList.Enqueue(this.transform.position);
        _firstNode.Speed = this.baseSpeed * 5;
    }

    private void Update()
    {
        AdjustRotation();
        AdjustPosition();
    }

    public void DropWaypoint()
    {
        NextNode.WaypointList.Enqueue(this.transform.position);
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
        ApplyRotationToTransform();
    }
    
    public void TurnVertical(float angleDir)
    {
        angleV += angleDir * turnSpeedV * Time.deltaTime;

        angleV = Mathf.Clamp(angleV, -verticalPivotThreshold, verticalPivotThreshold);
        ApplyRotationToTransform();
    }

    private void ApplyRotationToTransform()
    {
        rotH = Quaternion.AngleAxis(angleH, Vector3.up);
        rotV = Quaternion.AngleAxis(angleV, Vector3.right);

        this.transform.rotation = rotH * rotV;
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
