using _Code.CustomEvents.ColorEvent;
using _Code.CustomEvents.FloatEvent;
using _Code.CustomEvents.VoidEvent;
using UnityEngine;

namespace _Code.FlyingRocks
{
    [RequireComponent(typeof(Collider),typeof(MeshRenderer))]
    public class RockCollisionCallback : MonoBehaviour
    {
        [Tooltip("Leave as empty string if no tag is required")]
        [SerializeField] private string _targetTag = "";
        [SerializeField] private VoidEvent _stoneCollisionCallback;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.enabled)
            {
                return;
            }
            
            if (_targetTag == "")
            {
                _stoneCollisionCallback?.Raise();
                return;
            }
            
            if (other.CompareTag(_targetTag))
            {
                _stoneCollisionCallback?.Raise();
            }
        }
    }
}