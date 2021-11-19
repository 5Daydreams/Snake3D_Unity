using _Code.Scriptables.SimpleValues;
using UnityEngine;

namespace _Code.SimpleScripts.Timers
{
    public class Stopwatch : MonoBehaviour
    {
        [SerializeField] private bool _startOnEnable;
        [SerializeField] private FloatValue _timeElapsedScriptable;
        [SerializeField] private float _timeElapsed;
        private bool _isRunning = false;

        public float TimeElapsed
        {
            get
            {
                if (_timeElapsedScriptable != null)
                {
                    return _timeElapsedScriptable.Value;
                }
                else
                {
                    return _timeElapsed;
                }
            }

            set
            {
                if (_timeElapsedScriptable != null)
                {
                    _timeElapsedScriptable.Value = value;
                }
                else
                {
                    _timeElapsed = value;
                }
            }
        }

        private void OnEnable()
        {
            if (_startOnEnable)
            {
                StartTimer();
            }
        }

        private void Update()
        {
            if (!_isRunning)
                return;
            TimeElapsed += Time.deltaTime;
        }
    
        public void StopTimer()
        {
            _isRunning = false;
        }
    
        public void ResumeTimer()
        {
            _isRunning = true;
        }

        public void StartTimer()
        {
            TimeElapsed = 0;
            ResumeTimer();
        }
    }
}
