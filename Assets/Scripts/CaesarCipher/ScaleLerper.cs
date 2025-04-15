using UnityEngine;
using System.Collections;

public class ScaleLerper : MonoBehaviour
{
    [SerializeField] private float lerpDuration = 1f; // Duration of the scale animation in seconds
    
    private Vector3 initialScale;                     // Scale when object was spawned
    private Vector3 targetShrinkScale = new Vector3(0.0001f, 0.0001f, 0.0001f); // Tiny scale
    private bool isAnimating = false;                 // Prevent overlapping animations

    void Start()
    {
        // Record the initial scale when the object spawns
        initialScale = transform.localScale;
    }

    // Public method to shrink the object
    public void Shrink()
    {
        if (!isAnimating)
        {
            StartCoroutine(LerpScale(initialScale, targetShrinkScale));
        }
    }

    // Public method to grow the object back to initial scale
    public void Grow()
    {
        if (!isAnimating)
        {
            StartCoroutine(LerpScale(transform.localScale, initialScale));
        }
    }

    private IEnumerator LerpScale(Vector3 startScale, Vector3 endScale)
    {
        isAnimating = true;
        float elapsedTime = 0f;

        while (elapsedTime < lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);
            
            // Smoothly interpolate scale
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            
            yield return null;
        }

        // Ensure exact final scale
        transform.localScale = endScale;
        isAnimating = false;
    }
}