using DG.Tweening;
using TMPro;
using UnityEngine;

namespace S.Gameplay.G.Grid
{
    public class LetterCellView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _letterText;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetLetter(char letter)
        {
            _letterText.text = letter.ToString();
        }

        public void PlayAppearAnimation(float delay = 0f)
        {
            _canvasGroup.alpha = 0;
            _rectTransform.localScale = Vector3.zero;

            _canvasGroup.DOFade(1, 0.3f).SetDelay(delay);
            _rectTransform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetDelay(delay);
        }
    }
}
