using Unity.Cinemachine;
using UnityEngine;

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

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        public void ChangeToMainMenuCamera()
        {
            ToggleCameras(CamerasPosition.MainMenu);
        }

        public void ChangeToMainGameplayCamera()
        {
            ToggleCameras(CamerasPosition.MainGameplay);
        }

        public void ChangeToMerryGoRoundCamera()
        {
            ToggleCameras(CamerasPosition.MerryGoRound);
        }

        public void ChangeToRollerCoasterCamera()
        {
            ToggleCameras(CamerasPosition.RollerCoaster);
        }

        public void ChangeToFerrisWheelCamera()
        {
            ToggleCameras(CamerasPosition.FerrisWheel);
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