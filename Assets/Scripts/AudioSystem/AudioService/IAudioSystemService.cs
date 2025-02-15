using System.Collections.Generic;
using UnityEngine;

namespace Assets.AudioSystem.AudioService
{
    public interface IAudioSystemService
    {
        public AudioSource GetCurrentAudioSource { get; }
        public List<IAudioReceiver> AudioReceivers { get; }
        public void AddAudioReceiver(IAudioReceiver receiver);
        public void RemoveAudioReceiver(IAudioReceiver receiver);
    }
}