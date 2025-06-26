using DG.Tweening;
using S.MainMenu.MM.MVC;
using S.ScriptableObjects;
using UnityEngine;

namespace S.MapSystem.MVC
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private GameObject _background;
        //[SerializeField] private GameObject _header;
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void SetData(LevelData evtLevelData)
        {
            Debug.Log($"[LevelController] Level Data information : {evtLevelData.LevelId}");

            _mainMenuView.AnimateOut(AnimateLevelIn);
        }
        
        private void AnimateLevelIn()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
