using AudioSystem.AudioVisualizer;
using UnityEngine;

namespace AudioSystem.AudioService
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSystemHandler : MonoBehaviour, IAudioSystemService
    {
        private AudioSource _audioSource;
        private AudioPeerSystem _audioPeerSystem;
        public AudioSource GetCurrentAudioSource => _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioPeerSystem = GetComponent<AudioPeerSystem>();
        }
    }
}