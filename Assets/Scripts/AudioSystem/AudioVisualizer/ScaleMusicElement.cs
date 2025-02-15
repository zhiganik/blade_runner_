using System;
using Assets.Enums.AudioEnums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.AudioSystem.AudioVisualizer
{
    public class ScaleMusicElement : MonoBehaviour
    {
        [Header("Audio Control")] 
        [SerializeField] private SpectrumStreamType spectrumStreamType;
        [SerializeField] private AudioBandType audioBandType;
        [SerializeField] private bool useRandomAudioBand;
        [Header("Scale Control"), Space(3f)]
        [SerializeField] private ScaleVector scaleVector;
        [SerializeField] private bool useRandomScale;
        [SerializeField] private float scaleMultiplier;

        private Vector3 _startScale;
        private float _spectrumValue;

        private void Start()
        {
            _startScale = transform.localScale;
            Initialize();
        }

        private void Initialize()
        {
            var audioBandCount = Enum.GetValues(typeof(AudioBandType)).Length;
            var scaleVectorCount = Enum.GetValues(typeof(ScaleVector)).Length;

            if (useRandomAudioBand)
            {
                audioBandType = (AudioBandType) Random.Range(0, audioBandCount);
            }

            if (useRandomScale)
            {
                scaleVector = (ScaleVector) Random.Range(0, scaleVectorCount);
            }
        }

        private void Update()
        {
            CalculateSpectrumValue();
            SpectrumScale();
        }

        private void SpectrumScale()
        {
            switch (scaleVector)
            {
                case ScaleVector.X:
                    ScaleDependX();
                    break;
                case ScaleVector.Y:
                    ScaleDependY();
                    break;
                case ScaleVector.Z:
                    ScaleDependZ();
                    break;
                case ScaleVector.All:
                    ScaleDependAll();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CalculateSpectrumValue()
        {
            switch (spectrumStreamType)
            {
                case SpectrumStreamType.BandBuffer:
                    _spectrumValue = AudioPeerSystem.BandBuffer[(int) audioBandType];
                    break;
                case SpectrumStreamType.AudioBand:
                    _spectrumValue = AudioPeerSystem.AudioBand[(int) audioBandType];
                    break;
                case SpectrumStreamType.AudioBandBuffer:
                    _spectrumValue = AudioPeerSystem.AudioBandBuffer[(int) audioBandType];
                    break;
                case SpectrumStreamType.Amplitude:
                    _spectrumValue = AudioPeerSystem.Amplitude;
                    break;
                case SpectrumStreamType.AmplitudeBuffer:
                    _spectrumValue = AudioPeerSystem.AmplitudeBuffer;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ScaleDependX()
        {
            var targetScaleValue = transform.localScale;
            
            var scaleValue = new Vector3(
                _spectrumValue * scaleMultiplier + _startScale.x,
                targetScaleValue.y,
                targetScaleValue.z);
            
            transform.localScale = scaleValue;
        }
        
        private void ScaleDependY()
        {
            var targetScaleValue = transform.localScale;
            
            var scaleValue = new Vector3(
                targetScaleValue.x,
                _spectrumValue * scaleMultiplier + _startScale.y,
                targetScaleValue.z);
            
            transform.localScale = scaleValue;
        }
        
        private void ScaleDependZ()
        {
            var targetScaleValue = transform.localScale;
            
            var scaleValue = new Vector3(
                targetScaleValue.x,
                targetScaleValue.y,
                _spectrumValue * scaleMultiplier + _startScale.z);
            
            transform.localScale = scaleValue;
        }
        
        private void ScaleDependAll()
        {
            var scaleValue = new Vector3(
                _spectrumValue * scaleMultiplier + _startScale.x,
                _spectrumValue * scaleMultiplier + _startScale.z,
                _spectrumValue * scaleMultiplier + _startScale.z);
            
            transform.localScale = scaleValue;
        }
    }
}