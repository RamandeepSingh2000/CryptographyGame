using UnityEngine;

namespace RollerCoaster
{
    public class GridCipherPuzzle : MonoBehaviour
    {
        [SerializeField] private DraggableCellsManager _draggableCellsManager;
        [SerializeField] private TextPuzzlesManager _textPuzzlesManager;
        [SerializeField] private string _keyWord;

        private int _foundLettersCount = 0;

        private void OnEnable()
        {
            _textPuzzlesManager.OnPuzzleCompleted += HandleOnPuzzleCompleted;
            _draggableCellsManager.OnCellDropped += HandleOnCellDropped;
        }

        private void OnDisable()
        {
            _textPuzzlesManager.OnPuzzleCompleted -= HandleOnPuzzleCompleted;
            _draggableCellsManager.OnCellDropped += HandleOnCellDropped;
        }

        private void HandleOnPuzzleCompleted(char value)
        {
            _draggableCellsManager.ShowCell(value.ToString().ToUpper()[0]);
        }

        private void HandleOnCellDropped(char value)
        {
            _foundLettersCount++;
            if (_foundLettersCount == _keyWord.Length)
                Debug.Log("Cipher puzzle completed!");
        }
    }
}