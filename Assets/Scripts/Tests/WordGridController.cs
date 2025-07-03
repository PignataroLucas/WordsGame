using System.Collections.Generic;
using S.Events.E.Transitions;
using S.Gameplay.G.Grid;
using S.Gameplay.G.Keyboard.K.Effects;
using S.ScriptableObjects;
using S.Utility.U.EventBus;
using S.Utility.U.ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tests
{
    public class WordGridController : MonoBehaviour
    {
        [SerializeField] private WordGridView _wordGridView;
        [SerializeField] private FlyingLetterView _flyingLetterPrefab;
        [SerializeField] private RectTransform _spawnPoint;
        [SerializeField] private Transform _flyingLettersLayer;
        [SerializeField] private WordGridView _leftGridView;
        [SerializeField] private WordGridView _rightGridView;

        
        private List<string> _validWords;
        private List<string> _remainingWords;
        private LevelData _levelData;

        private const string _testWordInvalid = "dogs"; 
        
        private bool _isCompleted;

        public void SetLevelWords(List<string> wordList, LevelData levelData)
        {
            _validWords = new List<string>(wordList);
            _remainingWords = new List<string>(wordList);
            _levelData = levelData;
        }

        public void TrySubmitWordFromRadial(string word, List<Vector3> spawnPositions)
        {
            if(_validWords.Contains(word))
            {
                AnimateWordPlacement(word, spawnPositions);
            }
            else
            {
                Debug.Log($"❌ Wrong word from radial: {word}");
            }
        }

        private void AnimateWordPlacement(string word, List<Vector3> spawnPositions = null)
        {
            var targetRow = FindTargetRowForWord(word);

            if (targetRow == null || !targetRow.IsEmpty)
            {
                Debug.LogWarning($"[WordGridController] Can't place the word: {word}: row occupied or non-existent.");
                return;
            }

            var targetCells = targetRow.GetCells();

            var delayPerLetter = 0.1f;
            var accumulatedDelay = 0f;
            var lettersCompleted = 0;
            var wordLength = word.Length;

            for (int i = 0; i < wordLength; i++)
            {
                var letter = word[i];
                var targetCell = targetCells[i];

                var flyingLetter = Instantiate(_flyingLetterPrefab, _flyingLettersLayer);
                flyingLetter.Setup(letter);

                Vector3 spawnPosition = _spawnPoint.position;
                if(spawnPositions != null && i < spawnPositions.Count)
                {
                    spawnPosition = spawnPositions[i];
                }

                flyingLetter.transform.position = spawnPosition;

                flyingLetter.AnimateToTarget(
                    targetCell.GetWorldPosition(),
                    accumulatedDelay,
                    () =>
                    {
                        targetCell.RevealLetter(letter);
                        lettersCompleted++;

                        if(lettersCompleted == wordLength)
                        {
                            Debug.Log($"Word '{word}' fully placed!");

                            targetRow.SetWord(word);
                            _remainingWords.Remove(word);

                            CheckIfUserHasCompletedTheGrid(_levelData);
                        }
                    }
                );

                accumulatedDelay += delayPerLetter;
            }
        }

        private void CheckIfUserHasCompletedTheGrid(LevelData levelData)
        {
            if(_remainingWords.Count == 0 && _wordGridView.IsGridComplete())
            {
                Services.WaitFor<IEventBus>(bus => { bus.Publish(new LevelCompletedEvent(levelData)); });
            }
        }
        
        private WordRowView FindTargetRowForWord(string word)
        {
            var row = _leftGridView.GetRowForWord(word);
            
            if(row != null && row.IsEmpty)
            {
                return row;
            }

            row = _rightGridView.GetRowForWord(word);
            if(row != null && row.IsEmpty)
            {
                return row;
            }

            return null;
        }
    }
}
