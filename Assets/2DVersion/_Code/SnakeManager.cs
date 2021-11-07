using System.Collections.Generic;
using UnityEngine;

namespace _2DVersion._Code
{
    public class SnakeManager : MonoBehaviour
    {
        [SerializeField] private float distanceBetween = 0.2f;
        [SerializeField] private float speed;
        [SerializeField] private float turnSpeed;
        [SerializeField] private List<GameObject> bodyParts = new List<GameObject>();
        private List<GameObject> snakeBody = new List<GameObject>();

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
            if (bodyParts.Count > 0)
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
            snakeBody[0].GetComponent<Rigidbody2D>().velocity = snakeBody[0].transform.right * (speed * Time.deltaTime);
            if (Input.GetAxis("Horizontal") != 0)
            {
                snakeBody[0].transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal")));
            }

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

        private void CreateBodyParts()
        {
            if (snakeBody.Count == 0)
            {
                GameObject temp = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);

                temp.GetComponent<Rigidbody2D>().gravityScale = 0;

                snakeBody.Add(temp);
                bodyParts.RemoveAt(0);
            }

            WaypointDropper markM = snakeBody[snakeBody.Count - 1].GetComponent<WaypointDropper>();

            if (countUp == 0)
            {
                markM.ClearMarkerList();
            }

            countUp += Time.deltaTime;

            if (countUp >= distanceBetween)
            {
                GameObject temp = Instantiate(bodyParts[0], markM.markerList[0].position, markM.markerList[0].rotation,
                    transform);

                temp.GetComponent<Rigidbody2D>().gravityScale = 0;

                snakeBody.Add(temp);
                bodyParts.RemoveAt(0);
                temp.GetComponent<WaypointDropper>().ClearMarkerList();
                countUp = 0;
            }
        }

        public void AddBodyParts(GameObject gObj)
        {
            bodyParts.Add(gObj);
        }
    }
}