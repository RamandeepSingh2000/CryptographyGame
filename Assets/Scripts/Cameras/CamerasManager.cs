using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Cameras
{
    public class CamerasManager : MonoBehaviour
    {
        enum CamerasPosition
        {
            MainMenu,
            MainGameplay,
            MerryGoRound,
            RollerCoaster,
            FerrisWheel
        }

        [SerializeField] private CinemachineCamera _mainMenuCamera;
        [SerializeField] private CinemachineCamera _mainGameplayCamera;
        [SerializeField] private CinemachineCamera _merryGoRoundCamera;
        [SerializeField] private CinemachineCamera _rollerCoasterCamera;

        [SerializeField] private CinemachineCamera _ferrisWheelCamera;

        public UnityEvent OnMainGameplayCameraFocused;
        public UnityEvent OnMenuCameraFocused;
        public UnityEvent OnAnyStationCameraFocused;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        public void ChangeToMainMenuCamera()
        {
            ToggleCameras(CamerasPosition.MainMenu);
            OnMenuCameraFocused?.Invoke();
        }

        public void ChangeToMainGameplayCamera()
        {
            ToggleCameras(CamerasPosition.MainGameplay);
            OnMainGameplayCameraFocused?.Invoke();
        }

        public void ChangeToMerryGoRoundCamera()
        {
            ToggleCameras(CamerasPosition.MerryGoRound);
            OnAnyStationCameraFocused?.Invoke();
        }

        public void ChangeToRollerCoasterCamera()
        {
            ToggleCameras(CamerasPosition.RollerCoaster);
            OnAnyStationCameraFocused?.Invoke();
        }

        public void ChangeToFerrisWheelCamera()
        {
            ToggleCameras(CamerasPosition.FerrisWheel);
            OnAnyStationCameraFocused?.Invoke();
        }

        private void ToggleCameras(CamerasPosition cameraPosition)
        {
            _mainMenuCamera.enabled = cameraPosition == CamerasPosition.MainMenu;
            _mainGameplayCamera.enabled = cameraPosition == CamerasPosition.MainGameplay;
            _merryGoRoundCamera.enabled = cameraPosition == CamerasPosition.MerryGoRound;
            _rollerCoasterCamera.enabled = cameraPosition == CamerasPosition.RollerCoaster;
            _ferrisWheelCamera.enabled = cameraPosition == CamerasPosition.FerrisWheel;
        }
    }
}