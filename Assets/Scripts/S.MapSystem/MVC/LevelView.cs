using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private GameObject _shuffleButton;
        [SerializeField] private WordGridView _leftGridView;
        [SerializeField] private WordGridView _rightGridView;

        public void SetData(LevelData evtLevelData)
        {
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

            CreateDinamicGrid(evtLevelData);

            _radialKeyboardView.BuildKeyboard(evtLevelData.GetUniqueLetters());
            AnimateShuffleButton();
            _mainMenuView.AnimateOut(AnimateLevelIn);
        }

        private void AnimateShuffleButton()
        {
            _shuffleButton.transform.localScale = Vector3.zero;
            var duration = 0.3f;
            _shuffleButton.transform.DOScale(1f, duration).SetEase(Ease.OutBack);
        }

        private void CreateDinamicGrid(LevelData evtLevelData)
        {
            var sortedWords = SortWords(evtLevelData.WordList);

            var leftWords = sortedWords.Take(evtLevelData.MaxLeftColumnCapacity).ToList();
            var rightWords = sortedWords.Skip(evtLevelData.MaxLeftColumnCapacity).ToList();

            _leftGridView.ClearGrid();
            _rightGridView.ClearGrid();

            _leftGridView.BuildGrid(leftWords, _rowPrefab, _cellPrefab, evtLevelData.CellBackgroundColor);
            _rightGridView.BuildGrid(rightWords, _rowPrefab, _cellPrefab, evtLevelData.CellBackgroundColor);
        }

        private void AnimateLevelIn()
        {
            _canvasGroup.DOFade(1f, 0.8f)
                .SetEase(Ease.OutQuad)
                .OnStart(() =>
                {
                    _canvasGroup.interactable = true;
                    _canvasGroup.blocksRaycasts = true;
                });

            _background.DOFade(1f, 0.8f)
                .SetEase(Ease.OutQuad);
        }
        

        public void AnimateOutToMenu(Action onComplete)
        {
            var fullSequence = DOTween.Sequence();

            var gridSequence = AnimateGridCellsOut();
            fullSequence.Append(gridSequence);

            var uiSequence = AnimateUIElementsOut();
            fullSequence.Append(uiSequence);

            fullSequence.AppendCallback(() =>
            {
                Debug.Log("Start fade out del Background");
                _mainMenuView.ResetView();
            });
            fullSequence.Append(AnimateBackgroundOut());

            fullSequence.OnComplete(() =>
            {
                Debug.Log("All Sequence Finished.");
                onComplete?.Invoke();
            });
        }

        private Sequence AnimateGridCellsOut()
        {
            var baseDelay = 0f;
            var delayIncrement = 0.08f;
            var fallDistance = -200f;
            var duration = 0.3f;

            var seq = DOTween.Sequence();

            foreach(var row in _leftGridView.GetAllRows())
            {
                foreach (var cell in row.GetCells())
                {
                    seq.Insert(baseDelay, cell.transform.DOLocalMoveY(fallDistance, duration).SetEase(Ease.InOutBack));
                    seq.Insert(baseDelay, cell.GetCanvasGroup().DOFade(0, duration));
                    baseDelay += delayIncrement;
                }
            }

            foreach(var row in _rightGridView.GetAllRows())
            {
                foreach (var cell in row.GetCells())
                {
                    seq.Insert(baseDelay, cell.transform.DOLocalMoveY(fallDistance, duration).SetEase(Ease.InOutBack));
                    seq.Insert(baseDelay, cell.GetCanvasGroup().DOFade(0, duration));
                    baseDelay += delayIncrement;
                }
            }

            return seq;
        }

        private Sequence AnimateUIElementsOut()
        {
            var duration = 0.4f;
            var seq = DOTween.Sequence();

            if(_radialKeyboardView != null)
            {
                seq.Join(_radialKeyboardView.transform.DOScale(Vector3.zero, duration).SetEase(Ease.InBack));
            }

            if(_shuffleButton != null)
            {
                seq.Join(_shuffleButton.transform.DOScale(Vector3.zero, duration).SetEase(Ease.InBack));
            }

            return seq;
        }

        private Tweener AnimateBackgroundOut()
        {
            return _canvasGroup.DOFade(0, 1.2f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _canvasGroup.alpha = 1f; 
                });
        }
        
        private List<string> SortWords(List<string> words)
        {
            return words
                .OrderBy(w => w.Length)
                .ThenBy(w => w, StringComparer.Ordinal)
                .ToList();
        }
    }
}
