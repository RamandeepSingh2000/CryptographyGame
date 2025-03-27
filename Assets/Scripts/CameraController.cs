using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachinePositionComposer _cinemachinePosition;
    [SerializeField] private CinemachineInputAxisController _cinemachineInputAxis;
    [SerializeField] private TMP_Text _cameraInstructionText;

    [SerializeField] private float _zoomSpeed = 1f;
    [SerializeField] private Vector2 _zoomLimits = new Vector2(40f, 60f);

    private bool _isEnabled = false;
    private CameraControls _cameraControls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _cameraControls = new CameraControls();
    }

    private void OnEnable()
    {
        _cameraControls.Control.Toggle.performed += Toggle;
        _cameraControls.Control.Zoom.performed += Zoom;

        MenuEvents.OnGameStart += Enable;
        MenuEvents.OnGoToMainMenu += Disable;
    }
    
    private void OnDisable()
    {
        _cameraControls.Control.Toggle.performed -= Toggle;
        _cameraControls.Control.Zoom.performed -= Zoom;
        
        MenuEvents.OnGameStart -= Enable;
        MenuEvents.OnGoToMainMenu -= Disable;
    }

    private void Enable()
    {
        _cameraControls.Enable();
    }

    private void Disable()
    {
        _cameraControls.Disable();
    }
    private void Toggle(InputAction.CallbackContext obj)
    {
        _isEnabled = !_isEnabled;
        _cinemachineInputAxis.enabled = _isEnabled;
        _cameraInstructionText.text = _isEnabled ? "[F] Disable Camera Control" : "[F] Enable Camera Control";
        Cursor.lockState = _isEnabled ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void Zoom(InputAction.CallbackContext obj)
    {
        if (!_isEnabled) return;
        _cinemachinePosition.CameraDistance =
            Mathf.Clamp(_cinemachinePosition.CameraDistance + _zoomSpeed * obj.ReadValue<float>(), _zoomLimits.x,
                _zoomLimits.y);
    }
}