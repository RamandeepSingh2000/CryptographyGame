using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RollerCoaster
{
    public class CellSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private string _targetValue;

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
            if (eventData.pointerDrag == null) return;
            if (!eventData.pointerDrag.TryGetComponent(out DraggableCell draggableCell)) return;

            if (draggableCell.Value != _targetValue) return;

            draggableCell.AllowDrop(GetComponent<RectTransform>().position);
            GetComponent<Image>().raycastTarget = false;
        }
    }
}