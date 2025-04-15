using UnityEngine;
using System.Collections;

public class KarusellAnimator : MonoBehaviour
{
    [SerializeField] private GameObject oscillatingObject1; // First object to move
    [SerializeField] private GameObject oscillatingObject2; // Second object to move
    [SerializeField] private float rotationDuration = 60f;   // Time for 360° rotation (60s)
    [SerializeField] private float oscillationDuration = 5f; // Time for one full cycle (5s)
    [SerializeField] private AudioClip animationClip;       // Audio clip to play during animation
    [SerializeField] private AudioSource audioSource;       // AudioSource to play the clip

    private Coroutine rotationCoroutine;
    private Coroutine oscillationCoroutine;
    private Vector3 osc1StartPos;                           // Initial position of object 1
    private Vector3 osc2StartPos;                           // Initial position of object 2

    void Start()
    {
        if (oscillatingObject1 == null || oscillatingObject2 == null)
        {
            Debug.LogError("Oscillating objects not assigned in KarusellAnimator!");
            return;
        }

        if (animationClip == null)
        {
            Debug.LogWarning("No AudioClip assigned to KarusellAnimator!");
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Record initial positions
        osc1StartPos = oscillatingObject1.transform.localPosition;
        osc2StartPos = oscillatingObject2.transform.localPosition;
    }

    public void RunAnimations()
    {
        if (rotationCoroutine == null && oscillationCoroutine == null)
        {
            rotationCoroutine = StartCoroutine(RotateZAxis());
            oscillationCoroutine = StartCoroutine(OscillateObjects());
            
            // Play the audio clip if assigned
            if (animationClip != null && audioSource != null)
            {
                audioSource.clip = animationClip;
                audioSource.loop = true; // Loop the audio while animations run
                audioSource.Play();
                Debug.Log($"{name}: Audio started - Clip: {animationClip.name}");
            }

            Debug.Log($"{name}: Animations started");
        }
    }

    public void StopAnimations()
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }

        if (oscillationCoroutine != null)
        {
            StopCoroutine(oscillationCoroutine);
            oscillationCoroutine = null;

            // Reset positions to initial state
            oscillatingObject1.transform.localPosition = osc1StartPos;
            oscillatingObject2.transform.localPosition = osc2StartPos;
        }

        // Stop the audio if playing
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log($"{name}: Audio stopped");
        }

        Debug.Log($"{name}: Animations stopped");
    }

    private IEnumerator RotateZAxis()
    {
        while (true)
        {
            float elapsedTime = 0f;
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 0f, 180f);

            while (elapsedTime < rotationDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / rotationDuration);
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
                yield return null;
            }

            transform.rotation = targetRotation;
        }
    }

    private IEnumerator OscillateObjects()
    {
        while (true)
        {
            float elapsedTime = 0f;
            Vector3 targetUp = new Vector3(0, 0, 1f); // 1 unit up on Z-axis

            // Phase 1: Object1 goes up, Object2 goes down
            while (elapsedTime < oscillationDuration / 2f)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / (oscillationDuration / 2f));
                oscillatingObject1.transform.localPosition = Vector3.Lerp(osc1StartPos, osc1StartPos + targetUp, t);
                oscillatingObject2.transform.localPosition = Vector3.Lerp(osc2StartPos, osc2StartPos - targetUp, t);
                yield return null;
            }

            oscillatingObject1.transform.localPosition = osc1StartPos + targetUp;
            oscillatingObject2.transform.localPosition = osc2StartPos - targetUp;

            // Phase 2: Object1 goes down, Object2 goes up
            elapsedTime = 0f;
            while (elapsedTime < oscillationDuration / 2f)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / (oscillationDuration / 2f));
                oscillatingObject1.transform.localPosition = Vector3.Lerp(osc1StartPos + targetUp, osc1StartPos, t);
                oscillatingObject2.transform.localPosition = Vector3.Lerp(osc2StartPos - targetUp, osc2StartPos, t);
                yield return null;
            }

            oscillatingObject1.transform.localPosition = osc1StartPos;
            oscillatingObject2.transform.localPosition = osc2StartPos;
        }
    }
}