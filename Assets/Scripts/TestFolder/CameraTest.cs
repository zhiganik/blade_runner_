using UnityEngine;
using Cinemachine;
using ChunkSystem;

namespace InputSystem
{
    public class CameraTest : MonoBehaviour
    {
        [SerializeField] private Transform cameraTarget;

        public Transform noga;

        private ICinemachineCamera followCamera;

        private void Start()
        {
            followCamera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;
            followCamera.Follow = cameraTarget;
            followCamera.LookAt = cameraTarget;
        }
    }
}