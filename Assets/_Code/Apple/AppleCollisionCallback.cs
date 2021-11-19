using _Code.CustomEvents.ColorEvent;
using _Code.CustomEvents.FloatEvent;
using _Code.CustomEvents.IntEvent;
using UnityEngine;

namespace _Code.Apple
{
    [RequireComponent(typeof(Collider), typeof(MeshRenderer))]
    public class AppleCollisionCallback : MonoBehaviour
    {
        [Tooltip("Leave as empty string if no tag is required")] [SerializeField]
        private string _targetTag = "";

        [SerializeField] private ColorEvent _colorChangeCallback;
        [SerializeField] private FloatEvent _powerupCallback;
        [SerializeField] private float _powerupDuration = 5.0f;
        [SerializeField] private IntEvent _scoreCallback;
        [SerializeField] private int _scoreValue = 1;
        private Color _appleColor;

        private void Awake()
        {
            _appleColor = this.GetComponent<MeshRenderer>().sharedMaterial.color;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.enabled)
            {
                return;
            }

            if (_targetTag == "")
            {
                ResolveCallbacks();
                return;
            }

            if (other.CompareTag(_targetTag))
            {
                ResolveCallbacks();
            }
        }

        private void ResolveCallbacks()
        {
            if (_scoreCallback == null)
            {
                Debug.LogError("Apple has no score value associated!");
            }
            
            _scoreCallback?.Raise(_scoreValue);
            _colorChangeCallback?.Raise(_appleColor);
            _powerupCallback?.Raise(_powerupDuration);
            Destroy(this.gameObject);
        }
    }
}