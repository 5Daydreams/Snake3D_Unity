using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace _Code.SimpleScripts.Timers
{
    public abstract class TimeDistortMonoBehaviour : MonoBehaviour
    {
        // protected virtual void Start()
        // {
        //     SetupCustomUpdate();
        // }
        //
        // async void SetupCustomUpdate()
        // {
        //     bool active = true;
        //     while (true)
        //     {
        //         if (EditorApplication.isPaused)
        //         {
        //             if (active)
        //             {
        //                 Debug.Log("Paused!");
        //             }
        //
        //             active = false;
        //             continue;
        //         }
        //
        //         if (!active)
        //         {
        //             Debug.Log("Aaaand unpaused!");
        //         }
        //
        //         active = true;
        //
        //         float interval = Time.unscaledDeltaTime;
        //
        //         await Task.Delay(TimeSpan.FromSeconds(interval));
        //         CustomFixedUpdate();
        //
        //         bool exitCondition = !EditorApplication.isPlayingOrWillChangePlaymode;
        //
        //         if (exitCondition)
        //         {
        //             return;
        //         }
        //     }
        // }
        //
        // protected abstract void CustomFixedUpdate();
    }
}