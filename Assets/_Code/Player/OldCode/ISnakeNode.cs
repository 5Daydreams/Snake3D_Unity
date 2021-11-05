using System;
using System.Collections.Generic;
using UnityEngine;


public interface ISnakeNode
{
    public ISnakeNode NextNode { get; set; }
    public Queue<Vector3> WaypointList { get; set; }

    public ISnakeNode GetLastNode();
    public void SetSpeed(float speed);
}