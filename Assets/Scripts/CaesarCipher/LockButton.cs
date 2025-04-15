using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LockButton : MonoBehaviour
{
    [SerializeField] private MinuteHand _minuteHand;
    [SerializeField] private ClockHandsController[] _handControllers;
    [SerializeField] private Transform _hourHand;
    
    private Button _lockButton;
    private bool _isLocked = false;
    private Quaternion _savedMinuteRotation;
    private Quaternion _savedHourRotation;
    private const float _rotationDuration = 1f;
    private float _rotationProgress = 0f;
    private bool _isReturning = false;

    void Start()
    {
        _lockButton = GetComponent<Button>();
        if (_lockButton == null)
        {
            Debug.LogError("LockButton requires a Button component!");
            return;
        }
        
        if (_minuteHand == null || _hourHand == null || _handControllers.Length != 2)
        {
            Debug.LogError("Missing references in LockButton!");
            return;
        }

        _lockButton.onClick.AddListener(ToggleLockState);
    }

    void Update()
    {
        if (_isReturning)
        {
            _rotationProgress += Time.deltaTime / _rotationDuration;
            _minuteHand.transform.rotation = Quaternion.Slerp(_minuteHand.transform.rotation, 
                _savedMinuteRotation, _rotationProgress);
            _hourHand.rotation = Quaternion.Slerp(_hourHand.rotation, 
                _savedHourRotation, _rotationProgress);

            if (_rotationProgress >= 1f)
            {
                _isReturning = false;
                _rotationProgress = 0f;
                _minuteHand.transform.rotation = _savedMinuteRotation;
                _hourHand.rotation = _savedHourRotation;
                _minuteHand.IsLocked = true;
                foreach (var controller in _handControllers)
                {
                    controller.enabled = true;
                }
                _lockButton.interactable = true;
                Debug.Log("LockButton: Return complete, functionality restored");
            }
        }
    }

    private void ToggleLockState()
    {
        if (_isReturning) return;

        _isLocked = !_isLocked;
        _lockButton.interactable = false;

        if (_isLocked)
        {
            _minuteHand.IsLocked = false;
            foreach (var controller in _handControllers)
            {
                controller.enabled = false; // Temporarily disable to reset state
                controller.SetDualRotationMode(true, _hourHand);
                controller.enabled = true;  // Re-enable with new mode
            }
            _savedMinuteRotation = _minuteHand.transform.rotation;
            _savedHourRotation = _hourHand.rotation;
            _lockButton.interactable = true;
            //Debug.Log("LockButton: Locked - Both hands should rotate together now");
        }
        else
        {
            foreach (var controller in _handControllers)
            {
                controller.enabled = false; // Temporarily disable to reset state
                controller.SetDualRotationMode(false, null);
                controller.enabled = true;  // Re-enable with normal mode
            }
            _isReturning = true;
            _rotationProgress = 0f;
            //Debug.Log("LockButton: Unlocking - Returning to saved positions");
        }
    }

    void OnDestroy()
    {
        if (_lockButton != null)
        {
            _lockButton.onClick.RemoveListener(ToggleLockState);
        }
    }
}