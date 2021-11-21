using _Code.SimpleScripts.Timers;
using UnityEngine;

namespace _Code.Player
{
    public class FreeMovement3D : MonoBehaviour
    {
        [Header("Speed")] [SerializeField] private float baseSpeed;
        [SerializeField] private float boostMultiplier;

        [Header("Rotation")] [SerializeField] private float turnSpeedH = 120.0f;
        [SerializeField] private float turnSpeedV = 65.0f;
        [SerializeField] private float verticalPivotThreshold = 65.0f;
        [SerializeField] private float turnSmoothing = 1.0f;
        private float turnHeldTimeH = 0.0f;
        private float turnHeldTimeV = 0.0f;

        [HideInInspector] public float CurrentSpeed = 1.0f;
        private bool isBoosting;
        private Vector3 direction = Vector3.forward;

        private float angleH = 0;
        private float angleV = 0;
        Quaternion rotH = Quaternion.identity;
        Quaternion rotV = Quaternion.identity;

        private void Awake()
        {
            SetSpeedBoost(false);
        }

        private void FixedUpdate()
        {
            AdjustHeadRotation();
            AdjustHeadPosition();
        }
        
        void AdjustHeadRotation()
        {
            direction = this.transform.rotation * Vector3.forward;
        }

        void AdjustHeadPosition()
        {
            this.transform.position += direction * CurrentSpeed * Time.deltaTime;
        }

        public void TurnHorizontal(float angleDir)
        {
            float accelerationH = ApplyAccelerationH(angleDir);
                
            angleH += accelerationH * turnSpeedH * Time.deltaTime;
            ApplyRotationToHeadTransform();
            
            float ApplyAccelerationH(float inputValue)
            {
                if (inputValue == 0)
                {
                    turnHeldTimeH = 0;
                    return 0;
                }

                turnHeldTimeH += inputValue * Time.deltaTime;
                return Mathf.Clamp(turnHeldTimeH, -turnSmoothing, turnSmoothing)/turnSmoothing;
            }
        }

        public void TurnVertical(float angleDir)
        {
            float accelerationV = ApplyAccelerationV(angleDir);
                
            angleV += accelerationV * turnSpeedV * Time.deltaTime;

            angleV = Mathf.Clamp(angleV, -verticalPivotThreshold, verticalPivotThreshold);
            ApplyRotationToHeadTransform();
            
            float ApplyAccelerationV(float inputValue)
            {
                if (inputValue == 0)
                {
                    turnHeldTimeV = 0;
                    return 0;
                }
            
                turnHeldTimeV += inputValue * Time.deltaTime;
                return Mathf.Clamp(turnHeldTimeV, -turnSmoothing, turnSmoothing)/turnSmoothing;
            }
        }

        private void ApplyRotationToHeadTransform()
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
                CurrentSpeed = baseSpeed * boostMultiplier;
            }
            else
            {
                CurrentSpeed = baseSpeed;
            }
        }
    }
}