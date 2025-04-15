using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private Transform _alphabetClock;
    [SerializeField] private float _rotationDuration = 1f;
    [SerializeField] private Transform _decipherDisplay;
    
    private Button _button;
    private bool _isRotating = false;
    private Quaternion _startRotation;
    private Quaternion _targetRotation;
    private float _rotationProgress = 0f;

    void Start()
    {
        _button = GetComponent<Button>();
        if (_button == null)
        {
            Debug.LogError("SwitchButton requires a Button component!");
            return;
        }

        if (_alphabetClock == null)
        {
            Debug.LogError("_alphabetClock not assigned in SwitchButton!");
            return;
        }
        if (_decipherDisplay == null)
        {
            Debug.LogError("_decipherDisplay not assigned in SwitchButton!");
            return;
        }
        _decipherDisplay.localPosition = new Vector3(_decipherDisplay.localPosition.x, _decipherDisplay.localPosition.y, _decipherDisplay.localPosition.z * -1f);
        
        _button.onClick.AddListener(StartRotation);
    }

    void Update()
    {
        if (_isRotating)
        {
            _rotationProgress += Time.deltaTime / _rotationDuration;
            _alphabetClock.rotation = Quaternion.Slerp(_startRotation, _targetRotation, _rotationProgress);
            if (_rotationProgress >= 1f)
            {
                _isRotating = false;
                _rotationProgress = 0f;
                _alphabetClock.rotation = _targetRotation;
                _button.interactable = true;
            }
        }
    }

    private void StartRotation()
    {
        if (_isRotating) return;
        _button.interactable = false;
        _isRotating = true;
        _startRotation = _alphabetClock.rotation;
        _targetRotation = _startRotation * Quaternion.Euler(0f, 180f, 0f);
        _rotationProgress = 0f;
        _decipherDisplay.localPosition = new Vector3(_decipherDisplay.localPosition.x, _decipherDisplay.localPosition.y, _decipherDisplay.localPosition.z * -1f);
    }

    // Clean up
    void OnDestroy()
    {
        if (_button != null)
        {
            _button.onClick.RemoveListener(StartRotation);
        }
    }
}