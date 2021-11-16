using System;
using System.Collections.Generic;
using _Code.CustomEvents.VoidEvent;
using UnityEngine;
using UnityEngine.Events;

namespace _Code.SimpleScripts.BasicConditionTriggers
{
    [RequireComponent(typeof(Collider))]
    public class ColliderEnterCallback : MonoBehaviour
    {
        [Tooltip("Leave as empty list if no tag is required")] 
        [SerializeField] private List<string> _targetTags;
        [SerializeField] private bool _destroyOther;
        [SerializeField] private bool _destroySelf;
        [Space]
        [SerializeField] private UnityEvent _onTriggerEnterCallback;
        [SerializeField] private UnityEvent _onColliderEnterCallback;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.enabled)
            {
                return;
            }

            if (_targetTags.Count == 0)
            {
                ResolveTriggerCallbacks(other);
                return;
            }

            foreach (var collisionTag in _targetTags)
            {
                if (other.CompareTag(collisionTag))
                {
                    ResolveTriggerCallbacks(other);
                }
            }
        }

        private void ResolveTriggerCallbacks(Collider other)
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

        private void OnCollisionEnter(Collision other)
        {
            Collider otherCollider = other.collider;
            
            if (!otherCollider.enabled)
            {
                return;
            }

            if (_targetTags.Count == 0)
            {
                ResolveCollisionCallbacks(otherCollider);
                return;
            }

            foreach (var collisionTag in _targetTags)
            {
                if (otherCollider.CompareTag(collisionTag))
                {
                    ResolveCollisionCallbacks(otherCollider);
                }
            }
        }

        private void ResolveCollisionCallbacks(Collider other)
        {
            
        }
    }
}