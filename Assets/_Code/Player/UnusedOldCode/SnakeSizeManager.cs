using System.Collections.Generic;
using UnityEngine;

namespace _Code.Player.UnusedOldCode
{
    [RequireComponent(typeof(SnakeHeadMovement))]
    public class SnakeSizeManager : MonoBehaviour, ISnakeNode
    {
        [SerializeField] private SnakeBody _firstNode;
        private SnakeHeadMovement _head;
        
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

        public void SetSpeed(float speed)
        {
            Debug.LogError("Trying to Set the Speed of the head, nonono");
        }

        private void Awake()
        {
            WaypointList = new Queue<Vector3>();
            NextNode.WaypointList.Enqueue(this.transform.position);
            _head = this.GetComponent<SnakeHeadMovement>();
        }

        private void Update()
        {
            float headSpeed = _head.CurrentSpeed;
            
            NextNode.SetSpeed(headSpeed);
        }

        public void DropWaypoint()
        {
            // https://www.youtube.com/watch?v=sPlcecIh3ik
            NextNode.WaypointList.Enqueue(this.transform.position);
        }

    }
}