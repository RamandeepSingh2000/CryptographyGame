using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Cameras
{
    public class PlaceCamerasManager : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera[] _cameras;
        public bool IsOnMainCamera { get; private set; }
        public UnityEvent OnSwitchToMain;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            DisableAllCameras();
        }

        public void DisableAllCameras()
        {
            for (int i = 0; i < _cameras.Length; i++)
                _cameras[i].enabled = false;
            IsOnMainCamera = true;
        }

        public void SwitchToMain()
        {
            DisableAllCameras();
            OnSwitchToMain?.Invoke();
        }

        public void SwitchToCamera(int cameraIndex)
        {
            DisableAllCameras();
            if (cameraIndex >= 0 && cameraIndex < _cameras.Length)
                _cameras[cameraIndex].enabled = true;
            IsOnMainCamera = false;
        }
    }
}