using DG.Tweening;
using S.Gameplay.G.Grid;
using S.MainMenu.MM.MVC;
using S.ScriptableObjects;
using Tests;
using UnityEngine;

namespace S.MapSystem.MVC
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private WordGridView _wordGridView;
        [SerializeField] private WordGridController _wordGridController;
        [SerializeField] private GameObject _rowPrefab;
        [SerializeField] private GameObject _cellPrefab;
        
        public void SetData(LevelData evtLevelData)
        {
            Debug.Log($"[LevelController] Level Data information : {evtLevelData.LevelId}");

            _wordGridController.SetLevelWords(evtLevelData.WordList);

            _wordGridView.BuildGrid(
                evtLevelData.WordList,
                rowPrefab: _rowPrefab,
                cellPrefab: _cellPrefab
            );
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
