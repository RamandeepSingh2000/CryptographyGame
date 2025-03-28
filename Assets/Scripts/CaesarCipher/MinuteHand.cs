using UnityEngine;
using UnityEngine.Serialization;

public class MinuteHand : MonoBehaviour
{
    [SerializeField] private Transform _hourHand;
    [SerializeField] private Kcalculator _calculator;
    private float _minuteAngle = 0f;
    
    private const float _minuteIncrement = 15f;

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
    }

    public void TurnPlus()
    {
        // Rotate minute hand forward 15 degrees
        transform.Rotate(0f, 0f, -_minuteIncrement);
        _minuteAngle += _minuteIncrement;

        // Update hour hand based on total minute rotation
        UpdateHourHand();
    }

    public void TurnBack()
    {
        // Rotate minute hand backward 15 degrees
        transform.Rotate(0f, 0f, _minuteIncrement);
        _minuteAngle -= _minuteIncrement;

        // Update hour hand based on total minute rotation
        UpdateHourHand();
        
    }

    private void UpdateHourHand()
    {
        if (_hourHand == null) return;

        // Calculate hour rotation: 30 degrees per hour, where each 15° of minutes = 1/4 of 30°
        // 360° of minutes (1 full rotation) = 30° of hour movement
        float targetHourAngle = (15f/180f) * _minuteAngle;
        
        // Only update the Z rotation, preserving X and Y
        Vector3 currentRotation = _hourHand.localEulerAngles;
        _hourHand.localEulerAngles = new Vector3(
            currentRotation.x,
            currentRotation.y,
            -targetHourAngle // Negative for clockwise rotation
        );
        _calculator.CalculateAngleDifference();
    }
}
