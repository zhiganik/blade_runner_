using ChunkSystem;
using UnityEngine;
using Zenject;

namespace MonoInstallers
{
    public class ChunkSystemInstaller : MonoInstaller
    {
        [SerializeField] private ChunkSystemHandler chunkSystemHandler;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ChunkSystemHandler>().FromInstance(chunkSystemHandler).AsSingle();
        }
    }
}