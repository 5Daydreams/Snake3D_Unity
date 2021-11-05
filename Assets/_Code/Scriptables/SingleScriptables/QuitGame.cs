using UnityEngine;

namespace _Code.Scriptables.SingleScriptables
{
    [CreateAssetMenu(fileName = "QuitGame", menuName = "CustomScriptables/QuitGame")]
    public class QuitGame : ScriptableObject
    {
        public void Exit()
        {
            Application.Quit();
        }
    }
}