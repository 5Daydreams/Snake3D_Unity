using _Code.Scriptables.SimpleValues;
using UnityEngine;
using UnityEngine.Events;

namespace _Code.SimpleScripts.Timers
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private float startingTime;
        [SerializeField] private bool _repeats;
        [SerializeField] private FloatValue _timeRemaining;
        [SerializeField] private UnityEvent _onTimerFinishedSimple;
        [SerializeField] private CustomEvents.VoidEvent.VoidEvent _onTimerFinishedGlobal;
        private bool _isRunning = false;

        private void OnEnable()
        {
            if (_repeats)
            {
                StartCountdown();
            }
        }

        public void SetStartingTime(float time)
        {
            startingTime = time;
        }

        private void FixedUpdate()
        {
            if (_timeRemaining.Value <= 0)
            {
                _isRunning = false;
                
                if (_repeats)
                {
                    StartCountdown();
                }
                else
                {
                    _timeRemaining.Value = 0;
                }

                _onTimerFinishedSimple?.Invoke();
                _onTimerFinishedGlobal?.Raise();
            }
            
            if (!_isRunning)
                return;
            
            _timeRemaining.Value -= Time.deltaTime;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        public void ResumeTimer()
        {
            _isRunning = true;
        }

        public void StartCountdown()
        {
            _timeRemaining.Value = startingTime;
            ResumeTimer();
        }
    }
}