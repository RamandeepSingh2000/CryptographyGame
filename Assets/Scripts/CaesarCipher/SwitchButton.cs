using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private Transform _alphabetClock; // The object to rotate
    [SerializeField] private float rotationDuration = 1f; // Duration of rotation in seconds
    
    private Button button; // Reference to the Button component
    private bool isRotating = false; // Track if rotation is in progress
    private Quaternion startRotation; // Starting rotation
    private Quaternion targetRotation; // Target rotation
    private float rotationProgress = 0f; // Progress of rotation (0 to 1)

    void Start()
    {
        // Get the Button component
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("SwitchButton requires a Button component!");
            return;
        }

        if (_alphabetClock == null)
        {
            Debug.LogError("_alphabetClock not assigned in SwitchButton!");
            return;
        }

        // Set up the button click event
        button.onClick.AddListener(StartRotation);
    }

    void Update()
    {
        if (isRotating)
        {
            // Increment progress based on time
            rotationProgress += Time.deltaTime / rotationDuration;

            // Interpolate rotation
            _alphabetClock.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationProgress);

            // Check if rotation is complete
            if (rotationProgress >= 1f)
            {
                isRotating = false;
                rotationProgress = 0f;
                _alphabetClock.rotation = targetRotation; // Ensure exact final rotation
                button.interactable = true; // Re-enable button
            }
        }
    }

    private void StartRotation()
    {
        if (isRotating) return; // Prevent starting new rotation while one is in progress

        // Disable button
        button.interactable = false;

        // Set up rotation
        isRotating = true;
        startRotation = _alphabetClock.rotation;
        targetRotation = startRotation * Quaternion.Euler(0f, 180f, 0f); // Add 180° on Y-axis
        rotationProgress = 0f;
    }

    // Clean up
    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(StartRotation);
        }
    }
}