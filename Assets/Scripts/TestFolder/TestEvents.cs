using System;
using Assets.ChunkSystem;
using Assets.Enums.ChunkEnums;
using Assets.ObserverSystem;
using UnityEngine;
using Zenject;

namespace Assets.TestFolder
{
    public class TestEvents : MonoBehaviour, INotifyListener<ChunkCreationType>
    {
        private INotifyObserver<ChunkCreationType> _observer;
        private IChunkSystemService _chunkSystemService;

        [Inject]
        private void Construct(INotifyObserver<ChunkCreationType> observer, IChunkSystemService chunkSystemService)
        {
            _observer = observer;
            _chunkSystemService = chunkSystemService;
        }

        private void OnEnable()
        {
            _observer.AddListener(this);
        }

        private void OnDisable()
        {
            _observer.RemoveListener(this);
        }

        public void ReceiveNotification(ChunkCreationType state)
        {
            switch (state)
            {
                case ChunkCreationType.AddChunk:
                    Debug.Log("Chunk was added");
                    break;
                case ChunkCreationType.RemoveChunk:
                    Debug.Log("Chunk was deleted");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}