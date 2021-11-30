using AudioSystem.AudioService;
using InputSystem;
using UnityEngine;
using Zenject;

namespace ChunkSystem
{
    public class ParticleConnector : MonoBehaviour
    {
        [SerializeField] private ChunkPlatform chunkPlatform;
        [SerializeField] private AudioSystemHandler _audioSystemHandler;
        [SerializeField] private Transform affector;

        private ParticleSystemRenderer psr;
        private LivingParticlesAudioModule livingParticlesAudioModule;

        private void Start()
        {
            _audioSystemHandler = chunkPlatform.ChunkData.AudioSystemHandler;
            affector = chunkPlatform.ChunkData.CameraTest.noga;
            
            psr = GetComponent<ParticleSystemRenderer>();
        }
	
        private void Update ()
        {
            psr.material.SetVector("_Affector", affector.position);
        }
    }
}