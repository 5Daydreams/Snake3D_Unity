using _Code.CustomEvents.VoidEvent;
using UnityEngine;

namespace _Code.SimpleScripts.BasicConditionTriggers
{
    [RequireComponent(typeof(Collider))]
    public class TriggerOnCollision : MonoBehaviour
    {
        [Tooltip("Leave as empty string if no tag is required")]
        [SerializeField] private string _targetTag = "";
        [SerializeField] private VoidEvent _onTriggerEnterCallback;
        private void OnTriggerEnter(Collider other)
        {
            if (_targetTag == "")
            {
                _onTriggerEnterCallback?.Raise();
                return;
            }

            if (other.CompareTag(_targetTag))
            {
                _onTriggerEnterCallback?.Raise();
            }
        }
    }
}
