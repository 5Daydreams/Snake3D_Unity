using UnityEngine;
using UnityEngine.Events;

namespace _Code.CustomEvents
{
    public class TriggerOnUnityEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _awake;
        [SerializeField] private UnityEvent _onEnable;
        [SerializeField] private UnityEvent _reset;
        [SerializeField] private UnityEvent _start;
        [SerializeField] private UnityEvent _onDisable;
        [SerializeField] private UnityEvent _onDestroy;

        private void Awake()
        {
            _awake?.Invoke();
        }

        private void OnEnable()
        {
            _onEnable?.Invoke();
        }

        private void Reset()
        {
            _reset?.Invoke();
        }

        private void Start()
        {
            _start?.Invoke();
        }

        private void OnDisable()
        {
            _onDisable?.Invoke();
        }

        private void OnDestroy()
        {
            _onDestroy?.Invoke();
        }
    }
}