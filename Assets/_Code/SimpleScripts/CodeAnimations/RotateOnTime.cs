using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Code.SimpleScripts.CodeAnimations
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
                float x = Random.Range(-1, 1);
                float y = Random.Range(-1, 1);
                float z = Random.Range(-1, 1);
                
                _rotationAxis = new Vector3(x,y,z);
            }
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
    }
}
