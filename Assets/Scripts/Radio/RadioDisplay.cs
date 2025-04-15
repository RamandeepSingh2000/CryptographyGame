using System;
using TMPro;
using UnityEngine;

public class RadioDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;

    [SerializeField] private float scrollSpeed = 1f;

    [SerializeField] private float displayWidth = 0.4f;
    // Update is called once per frame
    void Update()
    {
        var rectTransform = displayText.rectTransform;
        var currentPosition = rectTransform.anchoredPosition;
        var width = displayText.rectTransform.rect.width;
        currentPosition.x -= Time.deltaTime * scrollSpeed;
        
        if (currentPosition.x < -width)
        {
            currentPosition.x = displayWidth;
        }
        rectTransform.anchoredPosition = currentPosition;
    }
    public void UpdateDisplay(string trackName, float channelFrequency)
    {
        displayText.text = $"{channelFrequency.ToString("0.##")} : [{trackName}]";
    }
}
