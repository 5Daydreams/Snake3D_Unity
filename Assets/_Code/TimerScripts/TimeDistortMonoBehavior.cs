using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace _Code.SimpleScripts.Timers
{
    public abstract class TimeDistortMonoBehavior : MonoBehaviour
    {
        protected virtual void Start()
        {
            SetupCustomUpdate();
        }

        async void SetupCustomUpdate()
        {
            while (true)
            {
                if (EditorApplication.isPaused)
                {
                    continue;
                }
                
                float interval = Time.unscaledDeltaTime;

                await Task.Delay(TimeSpan.FromSeconds(interval));
                CustomFixedUpdate();

                bool exitCondition = !EditorApplication.isPlayingOrWillChangePlaymode;
                
                if (exitCondition)
                {
                    return;
                }
            }
        }

        protected abstract void CustomFixedUpdate();
    }
}