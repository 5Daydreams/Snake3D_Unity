using System.Collections.Generic;
using _Code.LinkedList;
using _TutorialCode;
using UnityEngine;

namespace _Code.Player.LinkedListSnakeManager
{
    public class SnakeManagerLL : MonoBehaviour
    {
        [Header("Body Nodes Setup")] [SerializeField]
        private List<SnakeNode> bodyNodesSpawnQueue = new List<SnakeNode>();

        [SerializeField] private float distanceBetweenNodes = 0.2f;

        [Header("Speed")] [SerializeField] private float baseSpeed;
        [SerializeField] private float boostMultiplier;

        [Header("Rotation")] [SerializeField] private float turnSpeedH = 120.0f;
        [SerializeField] private float turnSpeedV = 65.0f;
        [SerializeField] private float verticalPivotThreshold = 65.0f;

        [HideInInspector] public float CurrentSpeed = 1.0f;
        private bool isBoosting;
        private Vector3 direction = Vector3.forward;

        private CustomLinkedList<SnakeNode> snakeBody = new CustomLinkedList<SnakeNode>();
        private float countUp = 0;

        private float angleH = 0;
        private float angleV = 0;
        Quaternion rotH = Quaternion.identity;
        Quaternion rotV = Quaternion.identity;

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

                SetSpeedBoost(false);

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
            AdjustHeadRotation();
            AdjustHeadPosition();

            if (snakeBody.Count > 1)
            {
                for (int i = 1; i < snakeBody.Count; i++)
                {
                    WaypointDropper frontNodeWPDropper = snakeBody.GetNodeAtIndex(i - 1).WaypointDropper;

                    snakeBody.GetNodeAtIndex(i).transform.position = frontNodeWPDropper.markerList[0].position;
                    snakeBody.GetNodeAtIndex(i).transform.rotation = frontNodeWPDropper.markerList[0].rotation;
                    frontNodeWPDropper.markerList.RemoveAt(0);

                    // Pseudo:
                    // Node A passes the 
                }
            }
        }

        void AdjustHeadRotation()
        {
            direction = snakeBody.Head.transform.rotation * Vector3.forward;
        }

        void AdjustHeadPosition()
        {
            snakeBody.Head.transform.position += direction * CurrentSpeed * Time.deltaTime;
        }

        public void TurnHorizontal(float angleDir)
        {
            angleH += angleDir * turnSpeedH * Time.deltaTime;
            ApplyRotationToHeadTransform();
        }

        public void TurnVertical(float angleDir)
        {
            angleV += angleDir * turnSpeedV * Time.deltaTime;

            angleV = Mathf.Clamp(angleV, -verticalPivotThreshold, verticalPivotThreshold);
            ApplyRotationToHeadTransform();
        }

        private void ApplyRotationToHeadTransform()
        {
            rotH = Quaternion.AngleAxis(angleH, Vector3.up);
            rotV = Quaternion.AngleAxis(angleV, Vector3.right);

            snakeBody.Head.transform.rotation = rotH * rotV;
        }

        public void SetSpeedBoost(bool value)
        {
            isBoosting = value;

            if (isBoosting)
            {
                CurrentSpeed = baseSpeed * boostMultiplier;
            }
            else
            {
                CurrentSpeed = baseSpeed;
            }
        }
    }
}