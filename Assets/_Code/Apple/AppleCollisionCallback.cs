using _Code.CustomEvents.ColorEvent;
using _Code.CustomEvents.VoidEvent;
using UnityEngine;

namespace _Code.Apple
{
    [RequireComponent(typeof(Collider))]
    public class AppleCollisionCallback : MonoBehaviour
    {
        [Tooltip("Leave as empty string if no tag is required")]
        [SerializeField] private string _targetTag = "";
        [SerializeField] private ColorEvent _onTriggerEnterCallback;
        [SerializeField] private Color _appleColor;
        private void OnTriggerEnter(Collider other)
        {
            if (_targetTag == "")
            {
                _onTriggerEnterCallback?.Raise(_appleColor);
                return;
            }

            if (other.CompareTag(_targetTag))
            {
                _onTriggerEnterCallback?.Raise(_appleColor);
            }
        }
    }
}