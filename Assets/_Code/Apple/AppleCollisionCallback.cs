using System;
using _Code.CustomEvents.ColorEvent;
using _Code.CustomEvents.VoidEvent;
using UnityEngine;

namespace _Code.Apple
{
    [RequireComponent(typeof(Collider),typeof(MeshRenderer))]
    public class AppleCollisionCallback : MonoBehaviour
    {
        [Tooltip("Leave as empty string if no tag is required")]
        [SerializeField] private string _targetTag = "";
        [SerializeField] private ColorEvent _onTriggerEnterCallback;
        private Color _appleColor;

        private void Awake()
        {
            _appleColor = this.GetComponent<MeshRenderer>().sharedMaterial.color;
        }

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