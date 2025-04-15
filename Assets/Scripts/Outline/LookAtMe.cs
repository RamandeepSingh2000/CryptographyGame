using System;
using Cameras;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CaesarCipher
{
    public class LookAtMe : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private int _index;
        [SerializeField] private PlaceCamerasManager _placeCamerasManager;
        private bool _closeUp = false;
        private void Start()
        {
            if (_placeCamerasManager != null) return;
            Debug.LogError("No placeCamerasManager assigned!");
            gameObject.SetActive(false);
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            var k = _closeUp ? 0 : _index;
            _placeCamerasManager.SwitchToCamera(k);
            _closeUp = !_closeUp;
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
