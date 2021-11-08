using _Code.CustomEvents.BaseEvent;
using UnityEngine;

namespace _Code.CustomEvents.FloatEvent
{
    [CreateAssetMenu(menuName = "CustomScriptables/Events/FloatEvent", fileName = "FloatEvent")]
    public class FloatEvent : BaseGameEvent<float>
    {
    }
}