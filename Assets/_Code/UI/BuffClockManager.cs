using System.Collections;
using System.Collections.Generic;
using _Code.Scriptables.TrackableValue;
using UnityEngine;
using UnityEngine.UI;

public class BuffClockManager : MonoBehaviour
{
    [SerializeField] private Image _buffImageFill;
    [SerializeField] private Text _buffTimeText;
    [SerializeField] private TrackableFloat _currentBuffTimer;
    private float _buffStartingTime = 10.0f;
    private void Awake()
    {
        SetGraphicsActive(false);
    }

    public void ActivateBuffVisual(float duration)
    {
        _buffStartingTime = duration;
        RefreshGraphicsOnTimer(_buffStartingTime);
        SetGraphicsActive(true);
    }

    public void RefreshGraphicsOnTimer(float timerValue)
    {
        if (timerValue <= 0)
        {
            SetGraphicsActive(false);
        }

        RefreshTimerSprite(timerValue);
        RefreshTimerText(timerValue);
        
        void RefreshTimerSprite(float value)
        {
            float fillValue = value / _buffStartingTime;
        
            _buffImageFill.fillAmount = fillValue;
        }
        
        void RefreshTimerText(float value)
        {
            _buffTimeText.text = value.ToString("F1");
        }
    }
    
    private void SetGraphicsActive(bool value)
    {
        _buffImageFill.gameObject.SetActive(value);
        _buffTimeText.gameObject.SetActive(value);
    }
}
