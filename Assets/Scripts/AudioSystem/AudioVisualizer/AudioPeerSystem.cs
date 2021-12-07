using System;
using UnityEngine;

namespace AudioSystem.AudioVisualizer
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPeerSystem : MonoBehaviour
    {
        public event Action<float[]> OnUpdateBuffer;

        [SerializeField] private float audioSmoothRateValue;
        [SerializeField] private FFTWindow encodingAudioAlgorithm;
        
        private AudioSource _audioSource;
        private float[] _samples;
        private float[] _frequencyBand;
        private float[] _bufferDecrease;
        private float[] _frequencyBandHighest;
        private float _amplitudeHighest;

        private const int SamplesCount = 512;
        private const int BandCount = 8;
        private const int MainChanel = 0;
        private const int StereoChanel = 1;
        
        public static float Amplitude;
        public static float AmplitudeBuffer;
        public static float[] AudioBandBuffer; // between 1 and 0 with smooth
        public static float[] AudioBand; // between 1 and 0 
        public static float[] BandBuffer; // simple band buffer


        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            InitializeAudioDataCaching();
            SetValueOfAudioProfile();
        }

        private void InitializeAudioDataCaching()
        {
            _samples = new float[SamplesCount];
            _frequencyBand = new float[BandCount];
            BandBuffer = new float[BandCount];
            _bufferDecrease = new float[BandCount];
            _frequencyBandHighest = new float[BandCount];
            AudioBand = new float[BandCount];
            AudioBandBuffer = new float[BandCount];
        }

        private void SetValueOfAudioProfile()
        {
            for (var i = 0; i < BandCount; i++)
            {
                _frequencyBandHighest[i] = audioSmoothRateValue;
            }
        }
        
        private void Update()
        {
            GetSpectrumData();
            MakeFrequencyBands();
            GenerateBandBuffer();
            CreateAudioBands();
            GetAmplitudeOfAllBands();

            OnUpdateBuffer.Invoke(AudioBandBuffer);
        }

        private void GetSpectrumData()
        {
            _audioSource.GetSpectrumData(_samples, MainChanel, encodingAudioAlgorithm);
        }

        private void GetAmplitudeOfAllBands()
        {
            float currentAmplitude = 0;
            float currentAmplitudeBuffer = 0;
            
            for (var i = 0; i < BandCount; i++)
            {
                currentAmplitude += AudioBand[i];
                currentAmplitudeBuffer += AudioBandBuffer[i];
            }

            if (currentAmplitude > _amplitudeHighest)
            {
                _amplitudeHighest = currentAmplitude;
            }

            Amplitude = currentAmplitude / _amplitudeHighest;
            AmplitudeBuffer = currentAmplitudeBuffer / _amplitudeHighest;
        }

        private void CreateAudioBands()
        {
            for (var i = 0; i < BandCount; i++)
            {
                if (_frequencyBand[i] > _frequencyBandHighest[i])
                {
                    _frequencyBandHighest[i] = _frequencyBand[i];
                }

                AudioBand[i] = (_frequencyBand[i] / _frequencyBandHighest[i]);
                AudioBandBuffer[i] = (BandBuffer[i] / _frequencyBandHighest[i]);
            }
        }

        private void GenerateBandBuffer()
        {
            for (var i = 0; i < 8; ++i)
            {
                if (_frequencyBand[i] > BandBuffer[i])
                {
                    BandBuffer[i] = _frequencyBand[i];
                    _bufferDecrease[i] = 0.005f;
                }

                if (!(_frequencyBand[i] < BandBuffer[i])) continue;
                
                BandBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f;
            }
        }
        
        private void MakeFrequencyBands()
        {
            /*
             22050 / 512 = 43 hertz per sample
             *
             20 - 60 hertz
             60 - 250 hertz
             250 - 500 hertz
             500 - 2000 hertz
             2000 - 4000 hertz
             4000 - 6000 hertz
             6000 - 20000 hertz
             *
             * 0 - 2 = 86 hertz
             * 1 - 4 = 172 hertz - 87-258
             * 2 - 8 = 344 hertz - 259-602
             * 3 - 16 = 688 hertz - 604-1290
             * 4 - 32 = 1376 hertz - 1291-2666
             * 5 - 64 = 2762 hertz - 2667-5418
             * 6 - 128 = 5504 hertz - 5419-10922
             * 7 - 256 = 11008 hertz - 10923-21930
             * 
             * total is 510
             */
            
            var count = 0;

            for (var i = 0; i < BandCount; i++)
            {
                float average = 0;
                var sampleCount = (int)Mathf.Pow(2, i) * 2;

                if (i == 7)
                {
                    sampleCount += 2;
                }

                for (var j = 0; j < sampleCount; j++)
                {
                    average += _samples[count] * (count + 1);
                    count++;
                }

                average /= count;
                _frequencyBand[i] = average * 10;
            }
        }
    }
}
