using AudioSystem.AudioVisualizer;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem.AudioService
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSystemHandler : MonoBehaviour, IAudioSystemService
    {
        private AudioSource _audioSource;
        private AudioPeerSystem _audioPeerSystem;
        public AudioSource GetCurrentAudioSource => _audioSource;
        public List<IAudioReceiver> AudioReceivers { get; }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioPeerSystem = GetComponent<AudioPeerSystem>();
        }

        public void NotifyAudioReceivers(List<IAudioReceiver> receivers)
        {
            for (int i = 0; i < receivers.Count; i++)
            {
                receivers[i].ReceiveAudioData();
            }
        }

        public void AddAudioReceiver(IAudioReceiver receiver)
        {
            AudioReceivers.Add(receiver);
        }

        public void RemoveAudioReceiver(IAudioReceiver receiver)
        {
            AudioReceivers.Remove(receiver);
        }
    }
}