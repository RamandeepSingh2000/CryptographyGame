using UnityEngine;
using UnityEngine.EventSystems;

namespace Outline
{
    public class Outliner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject[] _objectsToOutline;
        [SerializeField] private bool _changeHierarchy = false;

        public bool CanOutline = true;
        private int _outlineLayer;
        private int[] _objectsLayers;

        private void Start()
        {
            if (_objectsToOutline == null || _objectsToOutline.Length == 0)
                _objectsToOutline = new GameObject[] { gameObject };

            _objectsLayers = new int[_objectsToOutline.Length];
            for (int i = 0; i < _objectsToOutline.Length; i++)
                _objectsLayers[i] = _objectsToOutline[i].layer;

            _outlineLayer = LayerMask.NameToLayer("Outlines");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!CanOutline) return;
            ShowOutline(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ShowOutline(false);
        }

        public void EnableOutline() => CanOutline = true;
        public void DisableOutline() => CanOutline = false;

        private void ShowOutline(bool show)
        {
            for (int i = 0; i < _objectsToOutline.Length; i++)
            {
                if (_changeHierarchy)
                    SetLayerRecursively(_objectsToOutline[i], show ? _outlineLayer : _objectsLayers[i]);
                else _objectsToOutline[i].layer = show ? _outlineLayer : _objectsLayers[i];
            }
        }
        
        private void SetLayerRecursively(GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }
}