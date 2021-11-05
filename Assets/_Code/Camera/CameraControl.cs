using UnityEngine;

namespace _Code.Camera
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private float tilt;
        [SerializeField] private Vector3 baseLookOffset;
        private Vector3 lookOffset;

        void LateUpdate()
        {
            AdjustRotation();
            AdjustPositioning();
        }

        private void AdjustPositioning()
        {
            this.transform.position = target.position + this.transform.rotation * positionOffset;

            Vector3 direction = target.position + lookOffset - this.transform.position;
            // this.transform.rotation = Quaternion.LookRotation(direction);
        }

        private void AdjustRotation()
        {
            Quaternion thing = Quaternion.AngleAxis(tilt,target.right);
            this.transform.rotation = thing * target.rotation;
            return;
            //
            // float angle = target.transform.rotation.eulerAngles.y;
            // lookOffset = Quaternion.AngleAxis(angle, Vector3.up) * baseLookOffset;
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}