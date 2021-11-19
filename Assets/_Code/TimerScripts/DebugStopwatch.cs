using UnityEngine;

namespace _Code.SimpleScripts.Timers
{
    public class DebugStopwatch : MonoBehaviour
    {
        [SerializeField] private bool _startOnEnable;
        [SerializeField] private float _timeElapsedDeltaTime;
        [SerializeField] private float _timeElapsedUnscaledDeltaTime;
        [SerializeField] private float _timeElapsedFixedDeltaTime;
        [SerializeField] private float _timeElapsedFixedUnscaledDeltaTime	;
        private bool _isRunning = false;

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
            _timeElapsedDeltaTime += Time.deltaTime;
            _timeElapsedUnscaledDeltaTime += Time.unscaledDeltaTime;
            _timeElapsedFixedDeltaTime += Time.fixedDeltaTime;
            _timeElapsedFixedUnscaledDeltaTime += Time.fixedUnscaledDeltaTime;
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
            _timeElapsedDeltaTime = 0;
            _timeElapsedUnscaledDeltaTime = 0;
            _timeElapsedFixedDeltaTime = 0;
            _timeElapsedFixedUnscaledDeltaTime = 0;
            ResumeTimer();
        }
    }
}