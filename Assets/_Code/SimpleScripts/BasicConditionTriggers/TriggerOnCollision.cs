using System.Collections.Generic;
using _Code.CustomEvents.VoidEvent;
using UnityEngine;
using UnityEngine.Events;

namespace _Code.SimpleScripts.BasicConditionTriggers
{
    [RequireComponent(typeof(Collider))]
    public class TriggerOnCollision : MonoBehaviour
    {
        [Tooltip("Leave as empty list if no tag is required")] 
        [SerializeField] private List<string> _targetTags;
        [SerializeField] private bool _destroyOther;
        [SerializeField] private bool _destroySelf;
        [Space]
        [SerializeField] private UnityEvent _onTriggerEnterCallback;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.enabled)
            {
                return;
            }

            if (_targetTags.Count == 0)
            {
                ResolveCallbacks(other);
                return;
            }

            foreach (var collisionTag in _targetTags)
            {
                if (other.CompareTag(collisionTag))
                {
                    ResolveCallbacks(other);
                }
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