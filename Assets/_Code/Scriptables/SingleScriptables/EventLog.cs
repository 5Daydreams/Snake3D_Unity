using UnityEngine;

namespace _Code.Scriptables.SingleScriptables
{
    [CreateAssetMenu(menuName = "CustomScriptables/DebugMessager")]
    public class EventLog : ScriptableObject
    {
        public void ConsoleLog(string message)
        {
            Debug.Log(message);
        }
    }
}
