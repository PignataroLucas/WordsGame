using DG.Tweening;
using UnityEngine;

namespace S.MainMenu.MM.MVC
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void AnimateOut(System.Action onComplete)
        {
            _canvasGroup.DOFade(0, 0.8f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = false;
                    _canvasGroup.blocksRaycasts = false;
                    onComplete?.Invoke();
                });
        }

        public void ResetView()
        {
            _canvasGroup.DOFade(1, 0.8f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = true;
                    _canvasGroup.blocksRaycasts = true;
                });
        }
    }
}
