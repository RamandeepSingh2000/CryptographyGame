using UnityEngine;
using UnityEngine.InputSystem;

public class RadioInteractionHandler : MonoBehaviour
{
    PlayerInputActions inputActions;
    private Camera cam;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.PlayerActionMap.ClickAction.performed += ClickActionOnperformed;
        cam = Camera.main;
    }

    private void ClickActionOnperformed(InputAction.CallbackContext obj)
    {
        var mousePosition = inputActions.PlayerActionMap.MousePosition.ReadValue<Vector2>();
        var ray = cam.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent(out RadioButton radioButton))
            {
                radioButton.PerformAction();
            }
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
