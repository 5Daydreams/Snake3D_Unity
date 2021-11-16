using UnityEngine;
using UnityEngine.Events;

namespace _Code.Scriptables.SimpleValues
{
    [CreateAssetMenu(fileName = "IntTrackableValue",menuName = "CustomScriptables/TrackableValue/Int")]
    public class TrackableInt : ScriptableObject
    {
        [SerializeField] private int _value;
        [HideInInspector] public UnityEvent<int> CallbackOnValueChanged;

        public void SetValue(int overwrite)
        {
            _value = overwrite;
            CallbackOnValueChanged.Invoke(_value);
        }

        public void AddToValue(int addingValue)
        {
            SetValue(_value + addingValue);
        }
    }
}