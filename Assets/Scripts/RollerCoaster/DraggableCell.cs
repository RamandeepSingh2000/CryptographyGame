using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RollerCoaster
{
    public class DraggableCell : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private string _value;
        public Action OnCellDropped;

        private bool _isTaken = false;
        public string Value => _value;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        private Vector2 _initialPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _initialPosition = _rectTransform.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _canvasGroup.alpha = 0.6f;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            if (!_isTaken)
                RejectDrop();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void AllowDrop(Vector2 position)
        {
            _rectTransform.position = position;
            GetComponent<Image>().raycastTarget = false;
            _isTaken = true;
            OnCellDropped?.Invoke();
        }

        public void RejectDrop()
        {
            _rectTransform.DOAnchorPos(_initialPosition, 0.5f);
        }
    }
}