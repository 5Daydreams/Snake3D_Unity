using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Code.Scriptables.SingleScriptables
{
    [CreateAssetMenu(fileName = "NewSceneTransition", menuName = "CustomScriptables/SceneTransition")]
    public class SceneTransition : ScriptableObject
    {
        [SerializeField] private SceneAsset _sceneReference;
        [SerializeField] private bool _onlyTransition;
    
        public void ChangeScene()
        {
            if (_onlyTransition)
            {
                if (SceneManager.GetActiveScene().name == _sceneReference.name)
                    Debug.LogError("Error: this is a transition-only scene");
            }
            
            SceneManager.LoadScene(_sceneReference.name);
        }
    }
}
