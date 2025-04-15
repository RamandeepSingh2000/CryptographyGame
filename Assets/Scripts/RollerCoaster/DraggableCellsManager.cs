using System;
using UnityEngine;

namespace RollerCoaster
{
    public class DraggableCellsManager : MonoBehaviour
    {
        [Serializable]
        public class Cell
        {
            public char Value;
            public DraggableCell DraggableCell;
        }

        [SerializeField] private Cell[] _cells;
        public Action<char> OnCellDropped;

        private void Start()
        {
            for (int i = 0; i < _cells.Length; i++)
                _cells[i].DraggableCell.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            for (int i = 0; i < _cells.Length; i++)
                _cells[i].DraggableCell.OnCellDropped += HandleOnCellDropped;
        }

        private void OnDisable()
        {
            for (int i = 0; i < _cells.Length; i++)
                _cells[i].DraggableCell.OnCellDropped -= HandleOnCellDropped;
        }

        private void HandleOnCellDropped(char value)
        {
            OnCellDropped?.Invoke(value);
        }

        public void ShowCell(char value)
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                if (_cells[i].Value == value)
                {
                    _cells[i].DraggableCell.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
}