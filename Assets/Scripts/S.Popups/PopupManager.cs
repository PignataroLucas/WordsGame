using System.Threading.Tasks;
using DG.Tweening;
using S.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace S.Popups
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 0.4f;
        [SerializeField] private string layerKey;
        [SerializeField] private Button _closeButton;
        [SerializeField] private RectTransform _rectTransform;
        
        
        public string LayerKey => layerKey;
        
        private CanvasGroup _canvasGroup;
        private LevelData _levelData;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
            if(_closeButton != null)
            {
              _closeButton.onClick.AddListener(OnCloseButtonPressed);  
            }
        }

        public virtual void Prepare(LevelData levelData)
        {
            _levelData = levelData;
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

        private async void OnCloseButtonPressed()
        {
            await CloseAsync();
        }

        protected virtual async Task CloseAsync()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            var closeDuration = .4f;

            await _rectTransform
                .DOScale(Vector3.zero, closeDuration)
                .SetEase(Ease.InBack)
                .AsyncWaitForCompletion();

            GameObject.Destroy(gameObject);
        }
        
    }
}
