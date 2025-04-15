using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RollerCoaster
{
    public class DraggableCell : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private char _value;
        public Action<char> OnCellDropped;

        private bool _isTaken = false;
        public char Value => _value;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        private Vector2 _initialPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _initialPosition = _rectTransform.anchoredPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 0.6f;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            if (!_isTaken)
                RejectDrop();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void AllowDrop(Vector2 position)
        {
            _rectTransform.position = position;
            _isTaken = true;
            OnCellDropped?.Invoke(_value);
            _canvasGroup.enabled = false;
            enabled = false;
        }

        public void RejectDrop()
        {
            _rectTransform.DOAnchorPos(_initialPosition, 0.5f);
        }
    }
}