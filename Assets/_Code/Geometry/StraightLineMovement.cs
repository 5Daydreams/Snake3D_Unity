using UnityEngine;

namespace _Code.Geometry
{
    public class StraightLineMovement : MonoBehaviour
    {
        [Range(10.0f,20.0f)] [SerializeField] private float _speed = 15.0f;
        public Vector3 Direction = Vector3.one;

        private void Start()
        {
            Direction.Normalize();
        }

        void FixedUpdate()
        {
            if (Direction == Vector3.zero)
            {
                Direction = Vector3.one.normalized;
            }

            this.transform.position += Direction * (_speed * Time.smoothDeltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(transform.position,Direction * _speed);
        }
    }
}
