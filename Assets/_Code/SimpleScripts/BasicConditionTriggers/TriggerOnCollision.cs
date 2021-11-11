using _Code.CustomEvents.VoidEvent;
using UnityEngine;
using UnityEngine.Events;

namespace _Code.SimpleScripts.BasicConditionTriggers
{
    [RequireComponent(typeof(Collider))]
    public class TriggerOnCollision : MonoBehaviour
    {
        [Tooltip("Leave as empty string if no tag is required")]
        [SerializeField] private string _targetTag = "";
        [SerializeField] private bool _destroyOther;
        [SerializeField] private bool _destroySelf;
        [SerializeField] private UnityEvent _onTriggerEnterCallback;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.enabled)
            {
                return;
            }

            if (_targetTag == "")
            {
                ResolveCallbacks(other);
                return;
            }

            if (other.CompareTag(_targetTag))
            {
                ResolveCallbacks(other);
            }
        }

        private void ResolveCallbacks(Collider other)
        {
            _onTriggerEnterCallback?.Invoke();

            if (_destroyOther)
            {
                Destroy(other.gameObject);
            }
            
            if (_destroySelf)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
