using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
}
