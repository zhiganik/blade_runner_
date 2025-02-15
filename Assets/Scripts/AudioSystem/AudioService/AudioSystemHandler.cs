using System.Collections.Generic;
using Assets.AudioSystem.AudioVisualizer;
using UnityEngine;

namespace Assets.AudioSystem.AudioService
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSystemHandler : MonoBehaviour, IAudioSystemService
    {
        private AudioSource _audioSource;
        private AudioPeerSystem _audioPeerSystem;
        public AudioSource GetCurrentAudioSource => _audioSource;
        public List<IAudioReceiver> AudioReceivers { get; } = new List<IAudioReceiver>();

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioPeerSystem = GetComponent<AudioPeerSystem>();
        }

        public void AddAudioReceiver(IAudioReceiver receiver)
        {
            AudioReceivers.Add(receiver);
            _audioPeerSystem.OnUpdateBuffer += receiver.ReceiveAudioData;
        }

        public void RemoveAudioReceiver(IAudioReceiver receiver)
        {
            AudioReceivers.Remove(receiver);
            _audioPeerSystem.OnUpdateBuffer -= receiver.ReceiveAudioData;
        }
    }
}