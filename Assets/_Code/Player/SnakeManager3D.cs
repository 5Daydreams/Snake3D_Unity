using System.Collections.Generic;
using _TutorialCode;
using UnityEngine;

namespace _Code.Player
{
    public class SnakeManager3D : MonoBehaviour
    {
        [SerializeField] private float distanceBetween = 0.2f;
        [SerializeField] private List<GameObject> bodyNodesToSpawn = new List<GameObject>();
        private List<GameObject> snakeBody = new List<GameObject>();
    
        [Header("Speed")]
        [SerializeField] private float baseSpeed;
        [SerializeField] private float boostSpeed;

        [Header("Rotation")]
        [SerializeField] private float turnSpeedH = 90;
        [SerializeField] private float turnSpeedV = 50;
        [SerializeField] private float verticalPivotThreshold = 65.0f;

        private Vector3 direction = new Vector3(0,0,1);
        [HideInInspector] public float CurrentSpeed = 1.0f;
        private bool isBoosting;

        private float angleH = 0;
        private float angleV = 0;
        Quaternion rotH = Quaternion.identity;
        Quaternion rotV = Quaternion.identity;

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
            if (bodyNodesToSpawn.Count > 0)
            {
                CreateBodyParts();
            }

            for (int i = 0; i < snakeBody.Count; i++)
            {
                if (snakeBody[i] == null)
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

        private void SnakeMovement()
        {
            AdjustRotation();
            AdjustPosition();

            if (snakeBody.Count > 1)
            {
                for (int i = 1; i < snakeBody.Count; i++)
                {
                    WaypointDropper markM = snakeBody[i - 1].GetComponent<WaypointDropper>();

                    snakeBody[i].transform.position = markM.markerList[0].position;
                    snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                    markM.markerList.RemoveAt(0);
                }
            }
        }
    
        void AdjustRotation()
        {
            direction = snakeBody[0].transform.rotation * Vector3.forward;
        }

        void AdjustPosition()
        {
            snakeBody[0].transform.position += direction * CurrentSpeed * Time.deltaTime;
        }

        public void TurnHorizontal(float angleDir)
        {
            angleH += angleDir * turnSpeedH * Time.deltaTime;
            ApplyRotationToTransform();
        }
    
        public void TurnVertical(float angleDir)
        {
            angleV += angleDir * turnSpeedV * Time.deltaTime;

            angleV = Mathf.Clamp(angleV, -verticalPivotThreshold, verticalPivotThreshold);
            ApplyRotationToTransform();
        }

        private void ApplyRotationToTransform()
        {
            rotH = Quaternion.AngleAxis(angleH, Vector3.up);
            rotV = Quaternion.AngleAxis(angleV, Vector3.right);

            snakeBody[0].transform.rotation = rotH * rotV;
        }
    
        public void SetSpeedBoost(bool value)
        {
            isBoosting = value;
        
            if (isBoosting)
            {
                CurrentSpeed = baseSpeed + boostSpeed;
            }
            else
            {
                CurrentSpeed = baseSpeed;
            }
        }

        private void CreateBodyParts()
        {
            if (snakeBody.Count == 0)
            {
                GameObject temp = Instantiate(bodyNodesToSpawn[0], transform.position, transform.rotation);
                
                SetSpeedBoost(false);

                snakeBody.Add(temp);
                bodyNodesToSpawn.RemoveAt(0);
            }

            WaypointDropper markM = snakeBody[snakeBody.Count - 1].GetComponent<WaypointDropper>();

            if (countUp == 0)
            {
                markM.ClearMarkerList();
            }

            countUp += Time.deltaTime;

            if (countUp >= distanceBetween)
            {
                GameObject temp = Instantiate(bodyNodesToSpawn[0], markM.markerList[0].position, markM.markerList[0].rotation);

                snakeBody.Add(temp);
                bodyNodesToSpawn.RemoveAt(0);
                temp.GetComponent<WaypointDropper>().ClearMarkerList();
                countUp = 0;
            }
        }

        public void AddBodyParts(GameObject gObj)
        {
            bodyNodesToSpawn.Add(gObj);
        }
    }
}