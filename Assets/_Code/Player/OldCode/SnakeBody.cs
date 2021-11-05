using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Code.Player.OldCode
{
    public class SnakeBody : MonoBehaviour, ISnakeNode
    {
        private float _speed;
        private Vector3 _currentDirection;

        private Queue<Vector3> _waypointList = new Queue<Vector3>();
        public ISnakeNode NextNode { get; set; }
        public Queue<Vector3> WaypointList
        {
            get => _waypointList;
            set => _waypointList = value;
        }

        private const float WaypointDistanceThreshold = 0.001f;

        private void OnEnable()
        {
            WaypointList = new Queue<Vector3>();
        }

        public ISnakeNode GetLastNode()
        {
            if (NextNode == null)
            {
                return this;
            }

            return NextNode.GetLastNode();
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        private void Update()
        {
            FollowWaypoints();
        }

        private void FollowWaypoints()
        {
            if (WaypointList.Count == 0)
            {
                return;
            }

            Vector3 currentWaypoint = WaypointList.First();

            float distanceToNextWaypoint = Vector3.Distance(this.transform.position,currentWaypoint);
            
            if (distanceToNextWaypoint > WaypointDistanceThreshold)
            {
                _currentDirection = (currentWaypoint - transform.position).normalized;
                this.transform.position += _currentDirection * (Time.deltaTime * _speed);
            }
            else
            {
                Vector3 passedWaypoint = WaypointList.Dequeue();
                
                if (NextNode == null)
                {
                    return;
                }
                NextNode.WaypointList.Enqueue(passedWaypoint);
            }
        }
    }
}