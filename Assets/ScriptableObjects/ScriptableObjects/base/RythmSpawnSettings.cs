using AudioSystem.AudioVisualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    [CreateAssetMenu(fileName = "NewRythmSpawnSettings", menuName = "BladeRunner/RythmSpawnSettings")]
    public class RythmSpawnSettings : ScriptableObject
    {
        [SerializeField] private AudioBandType audioBandType;
        [Range(0f, 1f)]
        [SerializeField] private float targetAmplitude;
        [Range(0f, 1f)]
        [SerializeField] private float preventDelay;

        private DelayTimer delayTimer;
        private void OnEnable()
        {
            delayTimer = new DelayTimer();
        }

        public bool AllowSpawn(float[] channels)
        {
            if (channels[(int)audioBandType] >= targetAmplitude && delayTimer.CheckTimeOut(preventDelay))
            {
                return true;
            }
            return false;
        }
    }
}
