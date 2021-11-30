using UnityEngine;

namespace AudioSystem.AudioService
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSystemHandler : MonoBehaviour
    {
        [SerializeField] private LivingParticlesAudioSource livingParticlesAudioSource;
        
        private AudioSource _audioSource;

        public AudioSource AudioSource => _audioSource;
        public LivingParticlesAudioSource ParticleAudioSource => livingParticlesAudioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            livingParticlesAudioSource = GetComponent<LivingParticlesAudioSource>();
        }
    }
}