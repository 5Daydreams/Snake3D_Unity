using UnityEngine;

namespace _Code.Scriptables.TrackableValue
{
    [CreateAssetMenu(fileName = "FloatTrackableValue",menuName = "CustomScriptables/TrackableValue/Float")]
    public class TrackableFloat : Trackable<float>
    {
        public void AddToValue(float addingValue)
        {
            SetValue(_value + addingValue);
        }
    }
}