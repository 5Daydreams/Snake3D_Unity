using UnityEngine;

namespace _Code.Scriptables.SimpleValues
{
    public abstract class SimpleValue<T> : ScriptableObject
    {
        public T Value;

        public void SetValue(T overwrite)
        {
            Value = overwrite;
        }
    }
}