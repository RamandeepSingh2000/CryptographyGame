using NavKeypad;
using UnityEngine;

public class ControlStationInteractionHandler : MonoBehaviour
{
    private Camera cam;
    PlayerInputActions playerInputActions;
    ControlStation controlStation;
    private void Awake()
    {
        cam = Camera.main;
        controlStation = GetComponent<ControlStation>();
        playerInputActions = new();
        controlStation.OnEnterFocus.AddListener(playerInputActions.Enable);
        controlStation.OnExitFocus.AddListener(playerInputActions.Disable);
        controlStation.OnAccessGranted.AddListener(PermanentlyDisableInteraction);
        playerInputActions.PlayerActionMap.ClickAction.performed += PerformRaycast;
    }

    private void PerformRaycast(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var mousePosition = playerInputActions.PlayerActionMap.MousePosition.ReadValue<Vector2>();
        var ray = cam.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
            {
                keypadButton.PressButton();
            }
        }
    }
    public void PermanentlyDisableInteraction()
    {
        controlStation.OnEnterFocus.RemoveListener(playerInputActions.Enable);
        playerInputActions.Disable();
    }
}
