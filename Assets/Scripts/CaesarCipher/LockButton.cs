using UnityEngine;
using UnityEngine.UI;

public class LockButton : MonoBehaviour
{
    [SerializeField] private MinuteHand minuteHand;
    [SerializeField] private ClockHandsController[] handControllers;
    [SerializeField] private Transform hourHand;
    
    private Button lockButton;
    private bool isLocked = false;
    private Quaternion savedMinuteRotation;
    private Quaternion savedHourRotation;
    private float rotationDuration = 1f;
    private float rotationProgress = 0f;
    private bool isReturning = false;

    void Start()
    {
        lockButton = GetComponent<Button>();
        if (lockButton == null)
        {
            Debug.LogError("LockButton requires a Button component!");
            return;
        }
        
        if (minuteHand == null || hourHand == null || handControllers.Length != 2)
        {
            Debug.LogError("Missing references in LockButton!");
            return;
        }

        lockButton.onClick.AddListener(ToggleLockState);
    }

    void Update()
    {
        if (isReturning)
        {
            rotationProgress += Time.deltaTime / rotationDuration;
            minuteHand.transform.rotation = Quaternion.Slerp(minuteHand.transform.rotation, 
                savedMinuteRotation, rotationProgress);
            hourHand.rotation = Quaternion.Slerp(hourHand.rotation, 
                savedHourRotation, rotationProgress);

            if (rotationProgress >= 1f)
            {
                isReturning = false;
                rotationProgress = 0f;
                minuteHand.transform.rotation = savedMinuteRotation;
                hourHand.rotation = savedHourRotation;
                minuteHand.enabled = true;
                foreach (var controller in handControllers)
                {
                    controller.enabled = true;
                }
                lockButton.interactable = true;
                Debug.Log("LockButton: Return complete, functionality restored");
            }
        }
    }

    private void ToggleLockState()
    {
        if (isReturning) return;

        isLocked = !isLocked;
        lockButton.interactable = false;

        if (isLocked)
        {
            minuteHand.enabled = false;
            foreach (var controller in handControllers)
            {
                controller.enabled = false; // Temporarily disable to reset state
                controller.SetDualRotationMode(true, hourHand);
                controller.enabled = true;  // Re-enable with new mode
            }
            savedMinuteRotation = minuteHand.transform.rotation;
            savedHourRotation = hourHand.rotation;
            lockButton.interactable = true;
            Debug.Log("LockButton: Locked - Both hands should rotate together now");
        }
        else
        {
            foreach (var controller in handControllers)
            {
                controller.enabled = false; // Temporarily disable to reset state
                controller.SetDualRotationMode(false, null);
                controller.enabled = true;  // Re-enable with normal mode
            }
            isReturning = true;
            rotationProgress = 0f;
            Debug.Log("LockButton: Unlocking - Returning to saved positions");
        }
    }

    void OnDestroy()
    {
        if (lockButton != null)
        {
            lockButton.onClick.RemoveListener(ToggleLockState);
        }
    }
}