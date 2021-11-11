using _Code.CustomEvents.BaseEvent;
using UnityEngine;

namespace _Code.CustomEvents.ColorEvent
{
    [CreateAssetMenu(menuName = "CustomScriptables/Events/ColorEvent", fileName = "NewColorEvent")]
    public class ColorEvent : BaseGameEvent<Color>
    {
    }
}