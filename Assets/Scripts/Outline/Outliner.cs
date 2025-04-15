using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Outline
{
    public class Outliner : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        
        private Stack<GameObject> _highlightedObjects = new Stack<GameObject>();

        private void Update()
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            var hit = new RaycastHit();
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Interactable")))
            {
                var go = hit.collider.gameObject;
                go.layer = go.layer == LayerMask.NameToLayer("Interactable") ? LayerMask.NameToLayer("Outline") : LayerMask.NameToLayer("Interactable");
            }
            else
            {
                if (_highlightedObjects.Count > 0)
                {
                    //_highlightedObjects.Pop().GetComponent<OutlineBehaviour>().SetHighlighted(false);
                }
            }
            
        }
    }
}
