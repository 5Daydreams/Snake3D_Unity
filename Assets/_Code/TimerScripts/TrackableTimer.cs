using _Code.Scriptables.TrackableValue;
using UnityEngine;

public class TrackableTimer : MonoBehaviour
{
    [SerializeField] private TrackableFloat _trackableToCountdown;
    private bool _isRunning = false;

    public void StartFromGivenTime(float startTime)
    {
        _trackableToCountdown.Value = startTime;
        _isRunning = true;
    }

    private void FixedUpdate()
    {
        if (_trackableToCountdown.Value < 0)
        {
            _isRunning = false;
        }

        if (!_isRunning)
            return;

        // _trackableToCountdown.Value -= Time.deltaTime;
        _trackableToCountdown.Value -= Time.unscaledDeltaTime;
    }
}