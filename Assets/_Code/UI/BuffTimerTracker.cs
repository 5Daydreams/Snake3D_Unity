using System;
using _Code.Scriptables.TrackableValue;
using UnityEngine;
using UnityEngine.UI;

namespace _Code.UI
{
    public class BuffTimerTracker : MonoBehaviour
    {
        [SerializeField] private Image _buffImageFill;
        [SerializeField] private Text _buffTimeText;
        private float _buffStartingTime = 1.0f;
        private float _currentBuffTime;

        private void Awake()
        {
            SetGraphicsActive(false);
        }

        public void ApplyBuff(float duration)
        {
            _buffStartingTime = duration;
            _currentBuffTime = duration;
            
            SetGraphicsActive(true);
        }

        private void Update()
        {
            if (_currentBuffTime <= 0)
            {
                SetGraphicsActive(false);
                return;
            }
            
            RefreshTimerSprite(_currentBuffTime);
            RefreshTimerText(_currentBuffTime);
            _currentBuffTime -= Time.deltaTime;
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
