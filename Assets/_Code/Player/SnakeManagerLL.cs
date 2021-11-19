using System.Collections;
using System.Collections.Generic;
using _2DVersion._Code;
using _Code.LinkedList;
using _Code.Scriptables.TrackableValue;
using _Code.SimpleScripts.Timers;
using UnityEngine;

namespace _Code.Player
{
    public class SnakeManagerLL : MonoBehaviour
    {
        [SerializeField] private List<SnakeNode> _bodyNodesSpawnQueue = new List<SnakeNode>();
        [SerializeField] private TrackableInt _snakeSize;
        [SerializeField] private float _distanceBetweenNodes = 0.2f;

        private CustomLinkedList<SnakeNode> _snakeBody = new CustomLinkedList<SnakeNode>();
        private float _countUp = 0;

        private void Start()
        {
            CreateBodyParts();
        }

        private void FixedUpdate()
        {
            ManageSnakeBody();
            SnakeMovement();
        }

        private void ManageSnakeBody()
        {
            if (_bodyNodesSpawnQueue.Count > 0)
            {
                CreateBodyParts();
            }

            for (int i = 0; i < _snakeBody.Count; i++)
            {
                if (i == 0 && _snakeBody.Head == null)
                {
                    OnHeadDestroyed();
                    _snakeBody.Head = _snakeBody.GetNodeAtIndex(1);
                }

                if (_snakeBody.GetNodeAtIndex(i) == null)
                {
                    _snakeBody.RemoveAt(i);
                    i = i - 1;
                }
            }

            if (_snakeBody.Count == 0)
            {
                // Not necessary - snake is dead if count == 0 though
                Destroy(this);
            }
        }

        public void OnHeadDestroyed()
        {
            if (_snakeBody.Head == null)
            {
                return;
            }

            _snakeBody.Head.DisableAllColliders();
            Destroy(_snakeBody.Head.gameObject);
        }


        private void CreateBodyParts()
        {
            if (_snakeBody.Count == 0)
            {
                SnakeNode firstNode = Instantiate(_bodyNodesSpawnQueue[0], transform.position, transform.rotation);

                _snakeBody.Add(firstNode);
                _bodyNodesSpawnQueue.RemoveAt(0);
            }

            WaypointDropper tailWPDropper = _snakeBody.GetTail().WaypointDropper;

            if (_countUp == 0)
            {
                tailWPDropper.ClearMarkerList();
            }

            _countUp += Time.deltaTime;

            if (_countUp >= _distanceBetweenNodes)
            {
                SnakeNode newNode = Instantiate(_bodyNodesSpawnQueue[0], tailWPDropper.markerList[0].position,
                    tailWPDropper.markerList[0].rotation);

                _snakeBody.Add(newNode);
                _bodyNodesSpawnQueue.RemoveAt(0);
                newNode.WaypointDropper.ClearMarkerList();
                _countUp = 0;
            }
        }

        public void AddBodyParts(SnakeNode gObj)
        {
            _bodyNodesSpawnQueue.Add(gObj);
        }

        private void SnakeMovement()
        {
            if (_snakeBody.Count > 1)
            {
                for (int i = 1; i < _snakeBody.Count; i++)
                {
                    WaypointDropper frontNodeWPDropper = _snakeBody.GetNodeAtIndex(i - 1).WaypointDropper;

                    _snakeBody.GetNodeAtIndex(i).transform.position = frontNodeWPDropper.markerList[0].position;
                    _snakeBody.GetNodeAtIndex(i).transform.rotation = frontNodeWPDropper.markerList[0].rotation;
                    frontNodeWPDropper.markerList.RemoveAt(0);
                }
            }
        }

        public void NewHeadColor(Color newColour)
        {
            Color oldColor;
            SnakeNode currentNode = _snakeBody.Head;

            while (currentNode.Next != null)
            {
                oldColor = currentNode.CurrentNodeColor;
                currentNode.SetNodeColor(newColour);
                newColour = oldColor;

                currentNode = currentNode.Next;
            }
        }
    }
}