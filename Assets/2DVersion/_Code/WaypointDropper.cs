using System;
using System.Collections.Generic;
using UnityEngine;

namespace _2DVersion._Code
{
    public class WaypointDropper : MonoBehaviour
    {
        [HideInInspector] public List<Waypoint> markerList = new List<Waypoint>();
    
        private void FixedUpdate()
        {
            UpdateMarkerList();
        }

        public void UpdateMarkerList()
        {
            markerList.Add(new Waypoint(transform.position,transform.rotation));
        }

        public void ClearMarkerList()
        {
            markerList.Clear();
            markerList.Add(new Waypoint(transform.position,transform.rotation));
        }
    }

    [Serializable] public class Waypoint
    {
        public Vector3 position;
        public Quaternion rotation;

        public Waypoint(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }
}