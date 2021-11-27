using UnityEngine;

namespace AudioSystem.AudioService
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSystemHandler : MonoBehaviour
    {
        private AudioSource _audioSource;

        public AudioSource AudioSource => _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
}