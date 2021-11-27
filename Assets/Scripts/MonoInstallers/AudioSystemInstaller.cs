using AudioSystem.AudioService;
using UnityEngine;
using Zenject;

namespace MonoInstallers
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

            Container.Bind<AudioSystemHandler>().
                FromInstance(audioSystemInstance).
                AsSingle();
        }
    }
}