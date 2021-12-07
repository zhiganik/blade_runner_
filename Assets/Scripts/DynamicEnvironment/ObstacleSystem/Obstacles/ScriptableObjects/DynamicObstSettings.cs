using AudioSystem.AudioVisualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    [CreateAssetMenu(fileName = "NewDynamicObstSettings", menuName = "BladeRunner/DynamicObstacleSettings")]
    public class DynamicObstSettings : ScriptableObject
    {
        [SerializeField] private AudioBandType audioBandType;
        [SerializeField] private float _targetAmplitude;
        [SerializeField] private float _preventDelay;
        [SerializeField] private Vector3 _spawnPosition;
        [SerializeField] private float _startForce;
    }
}
