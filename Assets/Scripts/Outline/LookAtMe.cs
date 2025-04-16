using System;
using Cameras;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CaesarCipher
{
    public class LookAtMe : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private static LookAtMe currentLookAtMe;
        [SerializeField] private int _index;
        [SerializeField] private PlaceCamerasManager _placeCamerasManager;
        private bool _closeUp = false;
        [SerializeField] private UnityEvent onFocus;
        [SerializeField] private UnityEvent onUnfocus;
        private void Start()
        {
            if (_placeCamerasManager != null) return;
            Debug.LogError("No placeCamerasManager assigned!");
            gameObject.SetActive(false);
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (_closeUp)
            {
                _placeCamerasManager.DisableAllCameras();
                _closeUp = false;
                onUnfocus?.Invoke();
                return;
            }
            _placeCamerasManager.SwitchToCamera(_index);
            _closeUp = true;
            if (currentLookAtMe != null && currentLookAtMe != this)
            {
                currentLookAtMe.UnFocus();
            }
            onFocus?.Invoke();
            currentLookAtMe = this;
        }

        public void UnFocus()
        {
            _closeUp = false;
            onUnfocus?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(_closeUp) return;
            gameObject.layer = LayerMask.NameToLayer("Outlines");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
