using System;
using System.Threading.Tasks;
using UnityEngine;

namespace _Code.SimpleScripts.Timers
{
    public abstract class TimeDistortMonoBehavior : MonoBehaviour
    {
        private float _internalTimer;

        private void OnEnable()
        {
            float interval = Time.fixedUnscaledDeltaTime;
            SetupCustomUpdate(interval);
        }

        async void SetupCustomUpdate(double seconds)
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(seconds));
                CustomFixedUpdate();
                if (!Application.isPlaying)
                {
                    return;
                }
            }
        }

        protected abstract void CustomFixedUpdate();
    }
}