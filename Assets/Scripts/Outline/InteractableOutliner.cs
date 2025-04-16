using Outline;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class InteractableOutliner : Outliner, IPointerClickHandler
{
    [FormerlySerializedAs("_shouldEnableOutlineOnClick")] [SerializeField]private bool _shouldDisableOutlineOnClick = false;
    public UnityEvent OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_shouldDisableOutlineOnClick)
            CanOutline = false;
        OnClick?.Invoke();
    }
}
