using UnityEngine;
using UnityEngine.EventSystems;

public class CipherClockBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject highlightedClock; // The highlighted mesh
    [SerializeField] private Collider clockCollider;
    [SerializeField] private ScaleLerper _buttonsLerper;
    
    private bool isInteractable = true;                 // Tracks if the clock can be interacted with

    void Start()
    {
        if (highlightedClock == null)
        {
            Debug.LogError("HighlightedClock not assigned in CipherClockBehaviour!");
            return;
        }
        clockCollider = GetComponent<BoxCollider>();
        if (clockCollider == null)
        {
            clockCollider = gameObject.AddComponent<BoxCollider>();
        }

        // Initially hide the highlighted clock and disable interaction
        highlightedClock.SetActive(false);
        clockCollider.enabled = false;
        if (_buttonsLerper != null)
        {
            _buttonsLerper.Shrink();
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) return;

        highlightedClock.SetActive(true);
        //Debug.Log(eventData.position);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable) return;

        highlightedClock.SetActive(false);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable) return;
        isInteractable = false;
        highlightedClock.SetActive(false);
        if (_buttonsLerper != null)
        {
            _buttonsLerper.Grow();
        }
    }

    // Public method to enable interaction
    public void WakeMeUp()
    {
        isInteractable = true;
        clockCollider.enabled = true;
    }
}