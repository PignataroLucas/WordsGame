using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

namespace S.Gameplay.G.Keyboard.K.Effects
{
    public class FlyingLetterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _letterText;
        [SerializeField] private CanvasGroup _canvasGroup;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Setup(char letter)
        {
            _letterText.text = letter.ToString().ToUpper();
            _canvasGroup.alpha = 0;
            _rectTransform.localScale = Vector3.zero;
        }

        public void AnimateToTarget(Vector3 targetWorldPos, float delay, Action onArrived = null)
        {
            Sequence appearSeq = DOTween.Sequence();
            appearSeq.AppendInterval(delay);
            appearSeq.Append(_canvasGroup.DOFade(1f, 0.2f));
            appearSeq.Join(_rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack));

            appearSeq.Append(_rectTransform.DOMove(targetWorldPos, 0.5f).SetEase(Ease.InOutQuad));

            appearSeq.OnComplete(() =>
            {
                onArrived?.Invoke();
                Destroy(gameObject);
            });
        }
    }
}
