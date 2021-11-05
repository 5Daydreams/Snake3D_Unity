using System.Collections.Generic;
using UnityEngine;

namespace _Code.Player.OldCode
{
    public interface ISnakeNode
    {
        public ISnakeNode NextNode { get; set; }
        public Queue<Vector3> WaypointList { get; set; }

        public ISnakeNode GetLastNode();
        public void SetSpeed(float speed);
    }
}