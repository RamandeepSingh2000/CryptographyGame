using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace CaesarCipher
{
    public class CipherClockBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [FormerlySerializedAs("clockCollider")] [SerializeField] private Collider _clockCollider;
        [SerializeField] private ScaleLerper _buttonsLerper;
    
        private bool _isInteractable = true;                 // Tracks if the clock can be interacted with

        private void Start()
        {

            _clockCollider = GetComponent<BoxCollider>();
            if (_clockCollider == null)
            {
                _clockCollider = gameObject.AddComponent<BoxCollider>();
            }


            _clockCollider.enabled = false;
            if (_buttonsLerper != null)
            {
                _buttonsLerper.Shrink();
            }
        }
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isInteractable) return;

        }
    
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isInteractable) return;

        }
    
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            _isInteractable = false;
            if (_buttonsLerper != null)
            {
                _buttonsLerper.Grow();
            }
        }

        // Public method to enable interaction
        public void WakeMeUp()
        {
            _isInteractable = true;
            _clockCollider.enabled = true;
        }
    }
}