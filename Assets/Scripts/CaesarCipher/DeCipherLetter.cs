using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DeCipherLetter : MonoBehaviour
{
    [SerializeField] private TMP_Text _inputChar;
    [SerializeField] private TMP_Text _outputChar;
    [SerializeField] private Kcalculator _calculator;
    private float _hourAngle;
    
    public float HourAngle
    {
        set
        {
            _hourAngle = value;
            AngleToChar(_hourAngle);
        }
    }
    private void Start()
    {
        if (_calculator != null) return;
        _calculator = FindAnyObjectByType<Kcalculator>();
        if (_calculator != null) return;
    }
    
    private void AngleToChar(float angle)
    {
        Debug.Log(angle);
        // Invert the angle as it's coming inverted
        angle *= -1f;

        // Normalize the angle to a range of 0 to 360
        angle = (angle + 360f) % 360f;

        // Map the angle to the corresponding character
        int charIndex = Mathf.FloorToInt(angle / (360f / 26f)) % 26; // Divide by 360°/26 for proper ranges
        char charedAngle = (char)('A' + charIndex); // Convert to A-Z

        // Debug to verify mapping
        //Debug.Log($"Angle: {angle:F2} -> Index: {charIndex} -> Char: {charedAngle}");
        StartCoroutine(WaitAndContinue(0.5f, charedAngle));
    }

    /*public void DeCipherPosition(float angle, int k)
    {
        The angle, is comming from the rotation in z, of a game object, hence it goes from -180 to 180,
            same as before, each angle corresponds to a letter of the english alphabet so you can send it to DeCipher(char thing)
    }*/
    
    private void DeCipher(char inputChar)
    {
        var shift = _calculator.NormalizedValue;
        //Debug.Log($"Shift: {shift}");
        var outputChar = (char)(((inputChar - 'A' + shift) % 26) + 'A');
        _inputChar.text = inputChar.ToString();
        _outputChar.text = outputChar.ToString();
    }
    
    private IEnumerator WaitAndContinue(float waitTime, char charedAngle)
    {
        yield return new WaitForSeconds(waitTime);
        DeCipher(charedAngle);
    }
}
