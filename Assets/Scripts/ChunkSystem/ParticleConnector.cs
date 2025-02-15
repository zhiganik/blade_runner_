using Assets.ChunkSystem.ChunkElements;
using UnityEngine;

namespace Assets.ChunkSystem
{
    public class ParticleConnector : MonoBehaviour
    {
        [SerializeField] private ChunkPlatform chunkPlatform;
        [SerializeField] private Transform affector;

        private ParticleSystemRenderer _particleSystemRenderer;
        private static readonly int Affector = Shader.PropertyToID("_Affector");

        private void Start()
        {
            affector = chunkPlatform.CameraTest.noga;
            _particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        }
	
        private void Update ()
        {
            _particleSystemRenderer.material.SetVector(Affector, affector.position);
        }
    }
}