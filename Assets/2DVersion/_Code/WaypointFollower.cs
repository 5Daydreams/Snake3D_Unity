using System.Collections.Generic;
using UnityEngine;

namespace _2DVersion._Code
{
    public class WaypointFollower : MonoBehaviour
    {
        public Queue<Waypoint> WaypointList = new Queue<Waypoint>();
        private void Update()
        {
            WaypointMovement();
        }
        
        private void WaypointMovement()
        {
            Waypoint target = WaypointList.Dequeue();
            
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}