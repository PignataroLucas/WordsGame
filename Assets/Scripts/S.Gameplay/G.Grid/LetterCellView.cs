using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace S.Gameplay.G.Grid
{
    public class LetterCellView : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _letterText;
        [SerializeField] private Image _backgroundImage;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private Color _storedBackgroundColor;

        public string Letter => _letterText.text;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }
        
        
        public void SetBackgroundColor(Color color)
        {
            _storedBackgroundColor = color;
        }
        
        public void SetLetter(char c)
        {
            _letterText.text = c.ToString();
        }
        
        public CanvasGroup GetCanvasGroup()
        {
            return _canvasGroup;
        }

        public void Clear()
        {
            _letterText.text = "";
        }
        
        public void PlayAppearAnimation(float delay = 0f)
        {
            _canvasGroup.alpha = 0;
            _rectTransform.localScale = Vector3.zero;

            _canvasGroup.DOFade(1, 0.3f).SetDelay(delay);
            _rectTransform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetDelay(delay);
        }

        public void RevealLetter(char letter)
        {
            _letterText.text = letter.ToString();
            _canvasGroup.alpha = 0;
            transform.localScale = Vector3.zero;

            _backgroundImage.color = _storedBackgroundColor;

            _canvasGroup.DOFade(1f, 0.2f);
            transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        }

        public Vector3 GetWorldPosition()
        {
            return _rectTransform.position;
        }
    }
}
