using ChunkSystem;
using ChunkSystem.ChunkElements;
using UnityEngine;
using Zenject;

namespace MonoInstallers
{
    public class ChunkSystemInstaller : MonoInstaller
    {
        [SerializeField] private ChunkSystemHandler chunkSystemHandler;
        [SerializeField] private ChunkPlatform chunkPlatform;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ChunkSystemHandler>().FromInstance(chunkSystemHandler).AsSingle();
            Container.BindFactory<ChunkPlatform, DiChunkFactory>().FromComponentInNewPrefab(chunkPlatform).AsSingle();
        }
    }
}