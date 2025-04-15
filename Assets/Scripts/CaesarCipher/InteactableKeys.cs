using NavKeypad;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CaesarCipher
{
    public class InteractableKeys : MonoBehaviour, IPointerClickHandler
    {
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("No Main Camera found in scene!");
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Convert click position to ray
            var ray = cam.ScreenPointToRay(eventData.position);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                {
                    keypadButton.PressButton();
                    //Debug.Log($"Clicked button: {keypadButton.name}");
                }
                else
                {
                    //Debug.Log($"Hit object: {hit.collider.name}, but no KeypadButton found");
                }
            }
            else
            {
                //Debug.Log("Raycast hit nothing");
            }
        }
    }
}