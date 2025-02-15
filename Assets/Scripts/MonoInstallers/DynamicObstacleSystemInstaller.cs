using Assets.DynamicEnvironment.ObstacleSystem.@base;
using UnityEngine;
using Zenject;

namespace Assets.MonoInstallers
{
    public class DynamicObstacleSystemInstaller : MonoInstaller
    {
        [SerializeField] private DynamicObstacleSystem obstacleSystem;
        [SerializeField] private DynamicObstacle obstacle;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DynamicObstacleSystem>().FromInstance(obstacleSystem).AsSingle();
        }
    }
}

