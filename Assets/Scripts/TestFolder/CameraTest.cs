using Unity.Cinemachine;
using UnityEngine;

namespace Assets.TestFolder
{
    public class CameraTest : MonoBehaviour
    {
        [SerializeField] private Transform cameraTarget;

        public Transform noga;

        private CinemachineCamera followCamera;

        private void Start()
        {
            followCamera = GetFirstActiveCinemachineCamera();
            followCamera.Follow = cameraTarget;
            followCamera.LookAt = cameraTarget;
        }
        
        CinemachineCamera GetFirstActiveCinemachineCamera()
        {
            CinemachineBrain brain = Camera.main?.GetComponent<CinemachineBrain>();
            if (brain != null && brain.ActiveVirtualCamera != null)
            {
                return brain.ActiveVirtualCamera as CinemachineCamera;
            }
            return null;
        }
    }
}