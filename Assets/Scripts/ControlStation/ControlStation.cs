using System;
using Cameras;
using NavKeypad;
using UnityEngine;
using UnityEngine.Events;

public class ControlStation : MonoBehaviour
{
    [SerializeField] Keypad keypad;
    [SerializeField] private int secretCode = 12345;
    [SerializeField] private PlaceCamerasManager _placeCamerasManager;
    [SerializeField] private UnityEvent onEnterFocus;
    [SerializeField] private UnityEvent onExitFocus;
    [SerializeField] private UnityEvent onAccessDenied;
    [SerializeField] private UnityEvent onAccessGranted;
    public UnityEvent OnEnterFocus => onEnterFocus;
    public UnityEvent OnExitFocus => onExitFocus;
    public UnityEvent OnAccessDenied => onAccessDenied;
    public UnityEvent OnAccessGranted => onAccessGranted;
    
    public PlaceCamerasManager PlaceCamerasManager => _placeCamerasManager;
    public int SecretCode
    {
        get => secretCode;
        set
        {
            secretCode = value;
            keypad.ChangeKeypadCombo(secretCode);
        }
    }
    private void Awake()
    {
        keypad.OnAccessDenied.AddListener(OnAccessDeniedCallback);
        keypad.OnAccessGranted.AddListener(OnAccessGrantedCallback);
    }

    private void Start()
    {
        keypad.ChangeKeypadCombo(secretCode);
    }

    public void EnterFocus()
    {
        OnEnterFocus?.Invoke();
    }
    public void ExitFocus()
    {
        OnExitFocus?.Invoke();
    }
    
    private void OnAccessDeniedCallback()
    {
        onAccessDenied?.Invoke();
    }
    private void OnAccessGrantedCallback()
    {
        OnAccessGranted?.Invoke();
    }    
}
