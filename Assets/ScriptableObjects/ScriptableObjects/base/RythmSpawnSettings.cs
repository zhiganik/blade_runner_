using AudioSystem.AudioVisualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BladeRunner
{
    [CreateAssetMenu(fileName = "NewRythmSpawnSettings", menuName = "BladeRunner/RythmSpawnSettings")]
    public class RythmSpawnSettings : ScriptableObject
    {
        [SerializeField] private AudioBandType audioBandType;
        [Range(0f, 1f)]
        [SerializeField] private float targetAmplitude;
        [FormerlySerializedAs("preventDelay")]
        [Range(0f, 1f)]
        [SerializeField] private float spawnDelay;

        private DelayTimer _delayTimer;
        private void Awake()
        {
            _delayTimer = new DelayTimer(spawnDelay);
        }

        public bool AllowSpawn(float[] channels)
        {
            return (channels[(int) audioBandType] >= targetAmplitude &&
                    _delayTimer.IsTimeOut());
        }
    }
}
