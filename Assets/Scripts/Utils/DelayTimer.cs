using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public class DelayTimer
    {
        protected float FixedMoment;
        public DelayTimer()
        {
            FixedMoment = Time.time;
        }
        public bool CheckTimeOut(float delay)
        {
            if (Time.time >= (FixedMoment + delay))
            {
                FixedMoment = Time.time;
                return true;
            }
            return false;
        }
    }
}