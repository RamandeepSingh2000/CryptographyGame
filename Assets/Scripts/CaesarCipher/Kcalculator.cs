using TMPro;
using UnityEngine;

public class Kcalculator : MonoBehaviour
{
    [SerializeField] private Transform _minuteHand;
    [SerializeField] private Transform _hourHand;
    [SerializeField] private bool _useLocalRotation = true;
    [SerializeField] private TMP_Text _kVariable;

    public int NormalizedValue { get; private set; } 
    public float CurrentDifference { get; private set; }

    void Start()
    {
        if (_minuteHand == null || _hourHand == null)
        {
            Debug.LogError("One or both object references are not assigned!");
        }
    }

    public float CalculateAngleDifference()
    {
        if (_minuteHand == null || _hourHand == null) return 0f;

        float angle1 = _useLocalRotation ? _minuteHand.localEulerAngles.z : _minuteHand.eulerAngles.z;
        float angle2 = _useLocalRotation ? _hourHand.localEulerAngles.z : _hourHand.eulerAngles.z;

        // Calculate difference and ensure it's always positive
        CurrentDifference = (angle1 - angle2 + 360f) % 360f;
        
        NormalizedValue = Mathf.FloorToInt(CurrentDifference / 14.4f) + 1;
        NormalizedValue = Mathf.Clamp(NormalizedValue, 1, 26);

        LogDifference();
        return CurrentDifference;
    }

    [ContextMenu("Log Current Difference")]
    private void LogDifference()
    {
        _kVariable.text = "k = " + NormalizedValue;
    }
}