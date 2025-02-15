using Assets.TestFolder;
using UnityEngine;
using Zenject;

namespace Assets.MonoInstallers
{
    public class PlayerSystemInstaller : MonoInstaller
    {
        [SerializeField] private CameraTest playerPrefab;
        [SerializeField] private Transform spawnPoint;
        
        public override void InstallBindings()
        {
            var playerInstance =
                Container.InstantiatePrefabForComponent<CameraTest>(playerPrefab, spawnPoint.position,
                    Quaternion.identity, null);

            Container.Bind<CameraTest>().
                FromInstance(playerInstance).
                AsSingle();
        }
    }
}