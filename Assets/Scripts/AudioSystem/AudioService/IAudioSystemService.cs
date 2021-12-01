using UnityEngine;

namespace AudioSystem.AudioService
{
    public interface IAudioSystemService
    {
        public AudioSource GetCurrentAudioSource { get; }
    }
}