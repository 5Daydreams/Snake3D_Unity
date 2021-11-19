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
        private bool _isRunning = false;
        private float _timerIntenal = 0;

        public float TimerValue
        {
            get
            {
                if (_timeRemaining == null)
                {
                    return _timerIntenal;
                }

                return _timeRemaining.Value;
            }
            set
            {
                if (_timeRemaining == null)
                {
                    _timerIntenal = value;
                    return;
                }

                _timeRemaining.Value = value;
            }
        }

        private void OnEnable()
        {
            if (_repeats)
            {
                StartCountdown();
            }
        }

        public void StartFromGivenTime(float startTime)
        {
            SetStartingTime(startTime);
            StartCountdown();
        }

        public void SetStartingTime(float time)
        {
            startingTime = time;
        }

        private void FixedUpdate()
        {
            if (TimerValue <= 0)
            {
                _isRunning = false;
                
                if (_repeats)
                {
                    StartCountdown();
                }
                else
                {
                    TimerValue = 0;
                }

                _onTimerFinishedSimple?.Invoke();
            }
            
            if (!_isRunning)
                return;
            
            TimerValue -= Time.deltaTime;
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
            TimerValue = startingTime;
            ResumeTimer();
        }
    }
}