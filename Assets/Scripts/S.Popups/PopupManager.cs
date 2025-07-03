using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace S.Popups
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 0.4f;
        [SerializeField] private string layerKey;
        public string LayerKey => layerKey;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        }

        public virtual async Task ShowAsync()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            await _canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.OutQuad).AsyncWaitForCompletion();

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public virtual async Task CloseAsync()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            await _canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.InQuad).AsyncWaitForCompletion();

            gameObject.SetActive(false);
        }
    }
}
