using System;
using UnityEngine;

namespace RollerCoaster
{
    public class TextPuzzlesManager : MonoBehaviour
    {
        [SerializeField] private TextPuzzle[] _textPuzzles;
        [SerializeField] private GameObject[] _sentencesObject;
        public Action<char> OnPuzzleCompleted;

        private int _currentSentenceIndex = 0;

        private void OnEnable()
        {
            for (int i = 0; i < _textPuzzles.Length; i++)
                _textPuzzles[i].OnLetterFound += OnLetterFound;
        }

        private void OnDisable()
        {
            for (int i = 0; i < _textPuzzles.Length; i++)
                _textPuzzles[i].OnLetterFound -= OnLetterFound;
        }

        private void OnLetterFound(char letter)
        {
            OnPuzzleCompleted?.Invoke(letter);
        }

        public void PreviousSentence()
        {
            _currentSentenceIndex--;
            if (_currentSentenceIndex < 0)
                _currentSentenceIndex = _sentencesObject.Length - 1;
            UpdateSentence();
        }

        public void NextSentence()
        {
            _currentSentenceIndex++;
            if (_currentSentenceIndex > _sentencesObject.Length - 1)
                _currentSentenceIndex = 0;
            UpdateSentence();
        }

        private void UpdateSentence()
        {
            for (int i = 0; i < _sentencesObject.Length; i++)
                _sentencesObject[i].SetActive(i == _currentSentenceIndex);
        }
    }
}