using StaticObstacles;
using UnityEngine;
using Zenject;

namespace MonoInstallers
{
    public class StaticGenerationInstaller : MonoInstaller
    {
        [SerializeField] private StaticGenerationService chunkSystemHandler;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<StaticGenerationService>().FromInstance(chunkSystemHandler).AsSingle();
        }
    }
}