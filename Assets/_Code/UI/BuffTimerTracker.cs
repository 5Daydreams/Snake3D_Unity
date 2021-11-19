using _Code.Scriptables.TrackableValue;
using UnityEngine;
using UnityEngine.UI;

namespace _Code.UI
{
    public class BuffTimerTracker : MonoBehaviour
    {
        [SerializeField] private Image _buffImageFill;
        [SerializeField] private Text _buffTimeText;
        [SerializeField] private TrackableFloat _currentBuffTimer;
        private float _buffStartingTime = 1.0f;

        private void Awake()
        {
            SetGraphicsActive(false);
        }

        public void ApplyBuff(float duration)
        {
            _buffStartingTime = duration;
            _currentBuffTimer.Value = duration;
            
            SetGraphicsActive(true);
        }

        private void FixedUpdate()
        {
            if (_currentBuffTimer.Value <= 0)
            {
                SetGraphicsActive(false);
                return;
            }
            
            RefreshTimerSprite(_currentBuffTimer.Value);
            RefreshTimerText(_currentBuffTimer.Value);
            _currentBuffTimer.Value -= Time.deltaTime;
        }

        private void RefreshTimerSprite(float value)
        {
            float fillValue = value / _buffStartingTime;

            _buffImageFill.fillAmount = fillValue;
        }
        
        private void RefreshTimerText(float value)
        {
            _buffTimeText.text = value.ToString("F1");
        }

        private void SetGraphicsActive(bool value)
        {
            _buffImageFill.gameObject.SetActive(value);
            _buffTimeText.gameObject.SetActive(value);
        }
    }
}
