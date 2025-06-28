using DG.Tweening;
using S.Gameplay.G.Grid;
using S.Gameplay.G.Keyboard.K.Core;
using S.MainMenu.MM.MVC;
using S.ScriptableObjects;
using Tests;
using UnityEngine;
using UnityEngine.UI;

namespace S.MapSystem.MVC
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private WordGridView _wordGridView;
        [SerializeField] private WordGridController _wordGridController;
        [SerializeField] private GameObject _rowPrefab;
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private RadialKeyboardView _radialKeyboardView;
        
        public void SetData(LevelData evtLevelData)
        {
            
            Debug.Log($"[LevelController] Level Data information : {evtLevelData.LevelId}");

            string path = $"R.Images/I.Backgrounds/B.Levels/background_level_{evtLevelData.LevelId}";
            Sprite bgSprite = Resources.Load<Sprite>(path);

            if(bgSprite != null)
            {
                _background.sprite = bgSprite;
            }
            else
            {
                Debug.LogWarning($"[LevelView] Background image not found at path: {path}");
            }
            
            _wordGridController.SetLevelWords(evtLevelData.WordList);

            _wordGridView.BuildGrid(
                evtLevelData.WordList,
                rowPrefab: _rowPrefab,
                cellPrefab: _cellPrefab
            );
            
            _radialKeyboardView.BuildKeyboard(evtLevelData.GetUniqueLetters());
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
