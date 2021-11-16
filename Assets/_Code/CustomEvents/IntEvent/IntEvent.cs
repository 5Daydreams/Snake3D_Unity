using _Code.CustomEvents.BaseEvent;
using UnityEngine;

namespace _Code.CustomEvents.IntEvent
{
    [CreateAssetMenu(menuName = "CustomScriptables/Events/IntEvent", fileName = "IntEvent")]
    public class IntEvent : BaseGameEvent<int>
    {
    }
}