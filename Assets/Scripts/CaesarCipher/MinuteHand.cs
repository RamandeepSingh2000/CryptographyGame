using UnityEngine;
using UnityEngine.Serialization;

public class MinuteHand : MonoBehaviour
{
    [SerializeField] private Transform _hourHand;
    [SerializeField] private Kcalculator _calculator;
    [SerializeField] private DeCipherLetter _deCipherLetter;
    private float _targetHourAngle;
    [SerializeField] float _minuteAngle = 0f;

    private const float _minuteIncrement = 15f;
    private bool _isLocked = false;

    public float TargetHourAngle
    {
        get => _targetHourAngle;
        private set
        {
            _targetHourAngle = value;
            _deCipherLetter.HourAngle = _targetHourAngle;
        }
    }

    public bool IsLocked { get; set; }


    void Start()
    {
        if (_calculator == null)
        {
            _calculator = FindAnyObjectByType<Kcalculator>();
        }

        // Verify hour hand is assigned
        if (_hourHand == null)
        {
            Debug.LogError("Hour hand not assigned in inspector!");
        }

        if (_deCipherLetter != null) return;
        _deCipherLetter = FindAnyObjectByType<DeCipherLetter>();
    }

    public void TurnPlus()
    {
        transform.Rotate(0f, 0f, -_minuteIncrement);
        _minuteAngle += _minuteIncrement;
        UpdateHourHand();
    }

    public void TurnBack()
    {
        transform.Rotate(0f, 0f, _minuteIncrement);
        _minuteAngle -= _minuteIncrement;
        UpdateHourHand();
    }

    private void UpdateHourHand()
    {
        // Calculate hour rotation: 30 degrees per hour, where each 15° of minutes = 1/4 of 30°
        // 360° of minutes (1 full rotation) = 30° of hour movement
        TargetHourAngle = (15f / 180f) * _minuteAngle;

        // Only update the Z rotation, preserving X and Y
        Vector3 currentRotation = _hourHand.localEulerAngles;
        _hourHand.localEulerAngles = new Vector3(
            currentRotation.x,
            currentRotation.y,
            -TargetHourAngle // Negative for clockwise rotation
        );
        _calculator.CalculateAngleDifference();
    }
}