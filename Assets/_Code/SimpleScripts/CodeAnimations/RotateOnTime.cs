using UnityEngine;

namespace _Code.SimpleScripts.CodeAnimations
{
    public class RotateOnTime : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Vector3 _rotationAxis = Vector3.up;
        private Quaternion rotation = Quaternion.identity;
        
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
