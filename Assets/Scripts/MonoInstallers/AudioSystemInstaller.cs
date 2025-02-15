using Assets.AudioSystem.AudioService;
using UnityEngine;
using Zenject;

namespace Assets.MonoInstallers
{
    public class AudioSystemInstaller : MonoInstaller
    {
        [SerializeField] private AudioSystemHandler audioSystemHandler;

        public override void InstallBindings()
        {
            InitializeAudioSystem();
        }

        private void InitializeAudioSystem()
        {
            var audioSystemInstance =
                Container.InstantiatePrefabForComponent<AudioSystemHandler>(audioSystemHandler);
            
            Container.BindInterfacesAndSelfTo<AudioSystemHandler>().FromInstance(audioSystemInstance).AsSingle();
        }
    }
}