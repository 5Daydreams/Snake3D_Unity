using UnityEngine;
using UnityEngine.Events;

namespace _Code.FlyingRocks
{
    public class FloatConditionEventTrigger : MonoBehaviour
    {
        [SerializeField] private float _biggerThan = 0.0f;
        [SerializeField] private UnityEvent<float> _callbackOnTrue;
        [SerializeField] private UnityEvent<float> _callbackOnFalse;

        public void TryTriggerEvent(float value)
        {
            if (value > _biggerThan)
            {
                _callbackOnTrue.Invoke(value);
            }
            else
            {
                _callbackOnFalse.Invoke(value);
            }
        }
    }
}