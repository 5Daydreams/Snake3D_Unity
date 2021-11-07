using System;
using System.Collections.Generic;
using UnityEngine;

namespace _TutorialCode
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