using UnityEngine;

namespace Assets.Utils
{
    public class DelayTimer
    {
        private float _fixedMoment;
        private readonly float _delay;
        public DelayTimer(float delay)
        {
            _delay = delay;
            _fixedMoment = Time.time;
        }
        public bool IsTimeOut()
        {
            if (Time.time >= (_fixedMoment + _delay))
            {
                _fixedMoment = Time.time;
                return true;
            }
            
            _fixedMoment = Time.time;
            return false;
        }
    }
}