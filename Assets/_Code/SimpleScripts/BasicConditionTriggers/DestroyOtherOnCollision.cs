using _Code.CustomEvents.VoidEvent;
using UnityEngine;

namespace _Code.SimpleScripts.BasicConditionTriggers
{
    [RequireComponent(typeof(Collider))]
    public class DestroyOtherOnCollision : MonoBehaviour
    {
        [Tooltip("Leave as empty string if no tag is required")]
        [SerializeField] private string _targetTag = "";

        private void OnTriggerEnter(Collider other)
        {
            if (_targetTag == "")
            {
                Destroy(other.gameObject);
                return;
            }

            if (other.CompareTag(_targetTag))
            {
                Destroy(other.gameObject);
            }
        }
    }
}