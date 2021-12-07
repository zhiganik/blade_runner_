using AudioSystem.AudioVisualizer;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem.AudioService
{
    public interface IAudioSystemService
    {
        public AudioSource GetCurrentAudioSource { get; }
        public List<IAudioReceiver> AudioReceivers { get; }
        public void AddAudioReceiver(IAudioReceiver receiver);
        public void RemoveAudioReceiver(IAudioReceiver receiver);
    }
}