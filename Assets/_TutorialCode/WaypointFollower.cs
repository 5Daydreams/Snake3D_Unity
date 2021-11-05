using System;
using System.Collections.Generic;
using UnityEngine;

namespace _TutorialCode
{
    public class WaypointFollower : MonoBehaviour
    {
        public Queue<Waypoint> WaypointList;
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