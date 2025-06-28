using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace S.Gameplay.G.Keyboard.K.Core
{
    public class LetterButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _letterText;
        [SerializeField] private Image _highlightImage;
        
        private char _letter;
        private RectTransform _rectTransform;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            ClearHighlight();
        }

        public void SetLetter(char letter)
        {
            _letter = char.ToUpper(letter);
            _letterText.text = _letter.ToString();
        }

        public char GetLetter()
        {
            return _letter;
        }

        public RectTransform GetRectTransform()
        {
            return _rectTransform;
        }

        public void Highlight()
        {
            if (_highlightImage != null)
                _highlightImage.enabled = true;
        }

        public void ClearHighlight()
        {
            if (_highlightImage != null)
                _highlightImage.enabled = false;
        }
    }
}
