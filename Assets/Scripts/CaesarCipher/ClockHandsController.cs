using UnityEngine;
using UnityEngine.EventSystems;

public class ClockHandsController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private MinuteHand minuteHand;
    
    [SerializeField] private float startInterval = 0.3f;
    [SerializeField] private float minInterval = 0.05f;
    [SerializeField] private float accelerationRate = 0.5f;
    
    private float pressTime;
    private bool isPressed;
    private bool isTurnPlus;
    private float nextActionTime;
    private float currentInterval;
    private bool dualRotationMode = false;
    private Transform hourHandOverride;

    void Start()
    {
        if (minuteHand == null)
        {
            Debug.LogError("MinuteHand reference not assigned in ClockHandsController!");
        }
    }

    void Update()
    {
        if (isPressed && Time.time >= nextActionTime)
        {
            if (dualRotationMode && hourHandOverride != null && minuteHand != null)
            {
                float increment = isTurnPlus ? -15f : 15f;
                minuteHand.transform.Rotate(0f, 0f, increment);
                hourHandOverride.Rotate(0f, 0f, increment);
                Debug.Log($"{gameObject.name}: Dual rotation - Increment: {increment}");
            }
            else if (!dualRotationMode && minuteHand != null)
            {
                if (isTurnPlus) minuteHand.TurnPlus();
                else minuteHand.TurnBack();
                Debug.Log($"{gameObject.name}: Normal rotation - { (isTurnPlus ? "Plus" : "Back") }");
            }

            float holdTime = Time.time - pressTime;
            currentInterval = Mathf.Max(minInterval, startInterval - (accelerationRate * holdTime));
            nextActionTime = Time.time + currentInterval;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        pressTime = Time.time;
        nextActionTime = Time.time;
        currentInterval = startInterval;
        isTurnPlus = gameObject.name.Contains("Plus");
        Debug.Log($"{gameObject.name}: Pointer down - Dual mode: {dualRotationMode}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        Debug.Log($"{gameObject.name}: Pointer up");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void SetDualRotationMode(bool enabled, Transform hourHand)
    {
        dualRotationMode = enabled;
        hourHandOverride = hourHand;
        Debug.Log($"{gameObject.name}: Set dual mode to {enabled}, Hour hand: {(hourHand != null ? hourHand.name : "null")}");
    }

    [ContextMenu("Log Current Interval")]
    private void LogCurrentInterval()
    {
        Debug.Log($"Current Interval: {currentInterval:F3}s");
    }
}