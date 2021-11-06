using UnityEngine;

namespace _Code.Player.UnusedOldCode
{
    public class SnakeHeadMovement : MonoBehaviour
    {
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
    

        private void Update()
        {
            AdjustRotation();
            AdjustPosition();
        }
        void AdjustRotation()
        {
            direction = this.transform.rotation * Vector3.forward;
        }

        void AdjustPosition()
        {
            this.transform.position += direction * CurrentSpeed * Time.deltaTime;
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

            this.transform.rotation = rotH * rotV;
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
    }
}
