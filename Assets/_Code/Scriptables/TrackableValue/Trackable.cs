using UnityEngine;
using UnityEngine.Events;

namespace _Code.Scriptables.TrackableValue
{
    public abstract class Trackable<T> : ScriptableObject
    {
        [SerializeField] protected T _value;
        [HideInInspector] public UnityEvent<T> CallbackOnValueChanged;

        public void SetValue(T overwrite)
        {
            _value = overwrite;
            CallbackOnValueChanged.Invoke(_value);
        }
    }
}