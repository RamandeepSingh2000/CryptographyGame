using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayClue : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip clueClip;    // The audio clip to play
    [SerializeField] private AudioSource audioSource; // The AudioSource to play the clip
    
    private Button buttonComponent;                 // Optional: If using a Button instead of raw Image

    void Start()
    {
        // Check if there's a Button component (optional)
        buttonComponent = GetComponent<Button>();
        
        if (clueClip == null)
        {
            Debug.LogError("No AudioClip assigned to PlayClue!");
        }

        if (audioSource == null)
        {
            // Try to find an AudioSource on this GameObject or add one
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clueClip != null && audioSource != null && !audioSource.isPlaying)
        {
            PlayAudioClip();
        }
    }

    private void PlayAudioClip()
    {
        // Disable interaction
        if (buttonComponent != null)
        {
            buttonComponent.interactable = false;
        }
        else
        {
            // If no Button, disable the collider or image raycast target
            Graphic graphic = GetComponent<Graphic>();
            if (graphic != null)
            {
                graphic.raycastTarget = false;
            }
        }

        // Play the clip
        audioSource.PlayOneShot(clueClip);
        Debug.Log($"{name}: Playing clue clip - Duration: {clueClip.length}s");

        // Re-enable after clip finishes
        Invoke(nameof(EnableInteraction), clueClip.length);
    }

    private void EnableInteraction()
    {
        if (buttonComponent != null)
        {
            buttonComponent.interactable = true;
        }
        else
        {
            Graphic graphic = GetComponent<Graphic>();
            if (graphic != null)
            {
                graphic.raycastTarget = true;
            }
        }
        Debug.Log($"{name}: Interaction re-enabled");
    }
}