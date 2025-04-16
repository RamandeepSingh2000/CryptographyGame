using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RollerCoaster
{
    public class TextPuzzle : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private string _wrongWord;
        [SerializeField] private char _letter;
        private TextMeshProUGUI _textMesh;

        public Action<char> OnLetterFound;
        public Action OnWrongLetterClicked;

        private void Start()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int wordIndex =
                TMP_TextUtilities.FindIntersectingWord(_textMesh, eventData.position, eventData.pressEventCamera);
            int charIndex =
                TMP_TextUtilities.FindIntersectingCharacter(_textMesh, eventData.position, eventData.pressEventCamera,
                    true);
            var words = _textMesh.text.Split(" ");
            string word = words[Mathf.Clamp(wordIndex, 0, words.Length - 1)];
            char character = _textMesh.text[Mathf.Clamp(charIndex, 0, _textMesh.text.Length - 1)];
            Debug.Log("Word: " + word);
            Debug.Log("Character: " + character);
            if (word.Contains(_wrongWord) && character == _letter)
            {
                Debug.Log("Correct word and letter clicked!");
                var newText = _textMesh.text;
                newText = newText.Insert(charIndex + 1, "</color>");
                newText = newText.Insert(charIndex, "<color=red>");
                _textMesh.text = newText;
                OnLetterFound?.Invoke(character);
                OnLetterFound = null;
                enabled = false;
            }
            else
            {
                Debug.Log("Wrong word or letter clicked.");
                OnWrongLetterClicked?.Invoke();
            }
        }
    }
}