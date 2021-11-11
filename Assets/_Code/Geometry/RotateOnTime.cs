using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Code.Geometry
{
    public class RotateOnTime : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Vector3 _rotationAxis = Vector3.up;
        [SerializeField] private bool _randomizeRotationOnSpawn;
        private Quaternion rotation = Quaternion.identity;

        private void Awake()
        {
            if (_randomizeRotationOnSpawn)
            {
                float x = Random.Range(-1.0f, 1.0f);
                float y = Random.Range(-1.0f, 1.0f);
                float z = Random.Range(-1.0f, 1.0f);
                
                _rotationAxis = new Vector3(x,y,z);
            }
            
            _rotationAxis.Normalize();
        }

        void FixedUpdate()
        {
            if (_rotationAxis == Vector3.zero)
            {
                _rotationAxis = Vector3.up;
            }

            rotation = Quaternion.AngleAxis(Time.deltaTime * _rotationSpeed, _rotationAxis);
            this.transform.rotation = rotation * this.transform.rotation;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, _rotationAxis * _rotationSpeed / 180);
            Gizmos.color = Color.white;
        }
    }
}
