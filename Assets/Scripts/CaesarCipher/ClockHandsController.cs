using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ClockHandsController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private MinuteHand minuteHand;

    [SerializeField] private float _startInterval = 0.3f;
    [SerializeField] private float _minInterval = 0.05f;
    [SerializeField] private float _accelerationRate = 0.5f;

    private float _pressTime;
    private bool _isPressed;
    private bool _isTurnPlus;
    private float _nextActionTime;
    private float _currentInterval;
    private bool _dualRotationMode = false;
    private Transform _hourHandOverride;
    private DeCipherLetter _deCipherLetter;

    void Start()
    {
        if (minuteHand == null)
        {
            Debug.LogError("MinuteHand reference not assigned in ClockHandsController!");
        }
        
        _deCipherLetter = FindAnyObjectByType<DeCipherLetter>();
        if (_deCipherLetter == null)
        {
            Debug.LogError("DeCipherLetter reference not found in ClockHandsController!");
        }
    }

    void Update()
    {
        if (_isPressed && Time.time >= _nextActionTime)
        {
            if (_dualRotationMode && _hourHandOverride != null && minuteHand != null)
            {
                float increment = _isTurnPlus ? -15f : 15f;
                minuteHand.transform.Rotate(0f, 0f, increment);
                _hourHandOverride.Rotate(0f, 0f, increment);
                _deCipherLetter.HourAngle = -_hourHandOverride.rotation.eulerAngles.z;
                //Debug.Log($"{gameObject.name}: Dual rotation - Increment: {increment}");
            }
            else if (!_dualRotationMode && minuteHand != null)
            {
                if (_isTurnPlus) minuteHand.TurnPlus();
                else minuteHand.TurnBack();
                //Debug.Log($"{gameObject.name}: Normal rotation - { (_isTurnPlus ? "Plus" : "Back") }");
            }

            float holdTime = Time.time - _pressTime;
            _currentInterval = Mathf.Max(_minInterval, _startInterval - (_accelerationRate * holdTime));
            _nextActionTime = Time.time + _currentInterval;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        _pressTime = Time.time;
        _nextActionTime = Time.time;
        _currentInterval = _startInterval;
        _isTurnPlus = gameObject.name.Contains("Plus");
        //Debug.Log($"{gameObject.name}: Pointer down - Dual mode: {_dualRotationMode}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        //Debug.Log($"{gameObject.name}: Pointer up");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPressed = false;
    }

    public void SetDualRotationMode(bool enabled, Transform hourHand)
    {
        _dualRotationMode = enabled;
        _hourHandOverride = hourHand;
        //Debug.Log($"{gameObject.name}: Set dual mode to {enabled}, Hour hand: {(hourHand != null ? hourHand.name : "null")}");
    }

    [ContextMenu("Log Current Interval")]
    private void LogCurrentInterval()
    {
        //Debug.Log($"Current Interval: {_currentInterval:F3}s");
    }
}