using System.Collections.Generic;
using _2DVersion._Code;
using _Code.LinkedList;
using UnityEngine;

namespace _Code.Player
{
    public class SnakeManagerLL : MonoBehaviour
    {
        [SerializeField] private List<SnakeNode> bodyNodesSpawnQueue = new List<SnakeNode>();
        [SerializeField] private float distanceBetweenNodes = 0.2f;
        [SerializeField] private Color debugColorSelection;

        private CustomLinkedList<SnakeNode> snakeBody = new CustomLinkedList<SnakeNode>();
        private float countUp = 0;

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
            if (bodyNodesSpawnQueue.Count > 0)
            {
                CreateBodyParts();
            }

            for (int i = 0; i < snakeBody.Count; i++)
            {
                if (i == 0 && snakeBody.Head == null)
                {
                    snakeBody.Head = snakeBody.GetNodeAtIndex(1);
                }
                
                if (snakeBody.GetNodeAtIndex(i) == null)
                {
                    snakeBody.RemoveAt(i);
                    i = i - 1;
                }
            }

            if (snakeBody.Count == 0)
            {
                // Not necessary - snake is dead if count == 0 though
                Destroy(this);
            }
        }


        private void CreateBodyParts()
        {
            if (snakeBody.Count == 0)
            {
                SnakeNode firstNode = Instantiate(bodyNodesSpawnQueue[0], transform.position, transform.rotation);

                snakeBody.Add(firstNode);
                bodyNodesSpawnQueue.RemoveAt(0);
            }

            WaypointDropper tailWPDropper = snakeBody.GetTail().WaypointDropper;

            if (countUp == 0)
            {
                tailWPDropper.ClearMarkerList();
            }

            countUp += Time.deltaTime;

            if (countUp >= distanceBetweenNodes)
            {
                SnakeNode newNode = Instantiate(bodyNodesSpawnQueue[0], tailWPDropper.markerList[0].position,
                    tailWPDropper.markerList[0].rotation);

                snakeBody.Add(newNode);
                bodyNodesSpawnQueue.RemoveAt(0);
                newNode.WaypointDropper.ClearMarkerList();
                countUp = 0;
            }
        }

        public void AddBodyParts(SnakeNode gObj)
        {
            bodyNodesSpawnQueue.Add(gObj);
        }

        private void SnakeMovement()
        {
            if (snakeBody.Count > 1)
            {
                for (int i = 1; i < snakeBody.Count; i++)
                {
                    WaypointDropper frontNodeWPDropper = snakeBody.GetNodeAtIndex(i - 1).WaypointDropper;

                    snakeBody.GetNodeAtIndex(i).transform.position = frontNodeWPDropper.markerList[0].position;
                    snakeBody.GetNodeAtIndex(i).transform.rotation = frontNodeWPDropper.markerList[0].rotation;
                    frontNodeWPDropper.markerList.RemoveAt(0);
                }
            }
        }

        public void NewHeadColor(Color newColour)
        {
            Color oldColor;
            SnakeNode currentNode = snakeBody.Head;

            while (currentNode.Next != null)
            {
                oldColor = currentNode.CurrentNodeColor;
                currentNode.SetNodeColor(newColour);
                newColour = oldColor;
                
                currentNode = currentNode.Next;
            }
        }
        
        public void SetAllBodyColours()
        {
            for (int i = 0; i < snakeBody.Count; i++)
            {
                SetBodyColorAtIndex(i);
            }
        }

        public void SetBodyColorAtIndex(int index)
        {
            SnakeNode targetNode = snakeBody.GetNodeAtIndex(index);
            targetNode.SetNodeColor(debugColorSelection);
        }
    }
}