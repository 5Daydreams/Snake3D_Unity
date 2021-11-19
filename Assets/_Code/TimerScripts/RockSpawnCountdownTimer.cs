using UnityEngine;

namespace _Code.SimpleScripts.Timers
{
    public class RockSpawnCountdownTimer : CountdownTimer
    {
        [SerializeField] private float _minimumRepeatTime;
        public void ReduceCurrentRepeatTime(float reductionTime)
        {
            startingTime -= reductionTime;
            if (startingTime < _minimumRepeatTime)
            {
                startingTime = _minimumRepeatTime;
            }
        }
    }
}