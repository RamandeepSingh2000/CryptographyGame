using Unity.Cinemachine;
using UnityEngine;

namespace Cameras
{
    public class PlaceCamerasManager : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera[] _cameras;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            DisableAllCameras();
        }

        private void DisableAllCameras()
        {
            for (int i = 0; i < _cameras.Length; i++)
                _cameras[i].enabled = false;
        }

        public void SwitchToMain()
        {
            DisableAllCameras();
        }

        public void SwitchToCamera(int cameraIndex)
        {
            DisableAllCameras();
            if (cameraIndex >= 0 && cameraIndex < _cameras.Length)
                _cameras[cameraIndex].enabled = true;
        }
    }
}