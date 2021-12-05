using BladeRunner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MonoInstallers
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

