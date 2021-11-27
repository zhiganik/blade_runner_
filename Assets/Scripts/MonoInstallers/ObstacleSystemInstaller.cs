using BladeRunner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MonoInstallers
{
    public class ObstacleSystemInstaller : MonoInstaller
    {
        [SerializeField] private ObstacleSystem obstacleSystem;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObstacleSystem>().FromInstance(obstacleSystem).AsSingle();
        }
    }
}

