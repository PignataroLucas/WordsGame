using System.Collections.Generic;
using S.Events.E.Transitions;
using S.Gameplay.G.Grid;
using S.Gameplay.G.Keyboard.K.Effects;
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
        
        private List<string> _validWords;
        private List<string> _remainingWords;

        private string _testWordValid = "cats";   
        private string _testWordInvalid = "dogs"; 

        // public void SetLevelWords(List<string> wordList)
        // {
        //     _validWords = wordList;
        // }
        
        public void SetLevelWords(List<string> wordList)
        {
            _validWords = new List<string>(wordList);
            _remainingWords = new List<string>(wordList);
        }

        private void Update()
        {
            // if(Keyboard.current.aKey.wasPressedThisFrame)
            // { 
            //     TrySubmitWord(_testWordValid);
            // }
            //
            // if(Keyboard.current.bKey.wasPressedThisFrame)
            // { 
            //     TrySubmitWord(_testWordInvalid);
            // }
            
            if(Keyboard.current.aKey.wasPressedThisFrame)
            {
                SubmitRandomWord();
            }

            if(Keyboard.current.bKey.wasPressedThisFrame)
            {
                TrySubmitWord(_testWordInvalid);
            }
        }

        private void TrySubmitWord(string word)
        {
            if(_validWords.Contains(word))
            {
                _wordGridView.ClearPrevious();     
                //_wordGridView.FillNextEmpty(word); 
                AnimateWordPlacement(word);
            }
            else
            {
                Debug.Log($"❌ Wrong word : {word}");
            }
        }
        
        private void SubmitRandomWord()
        {
            if (_remainingWords.Count == 0)
            {
                Debug.Log("[WordGridController] No hay más palabras por completar.");
                return;
            }

            int index = UnityEngine.Random.Range(0, _remainingWords.Count);
            string randomWord = _remainingWords[index];

            AnimateWordPlacement(randomWord);

            _remainingWords.RemoveAt(index);

            if (_remainingWords.Count == 0 && _wordGridView.IsGridComplete())
            {
                Services.WaitFor<IEventBus>(bus =>
                {
                    bus.Publish(new LevelCompletedEvent());
                });
            }
        }
        
        private void AnimateWordPlacement(string word)
        {
            Debug.Log($"Animating word: {word}");

            var targetRow = _wordGridView.GetNextEmptyRow(word.Length);

            if (targetRow == null)
            {
                Debug.LogWarning("[WordGridController] No empty row found for this word length!");
                return;
            }

            var targetCells = targetRow.GetCells();

            float delayPerLetter = 0.1f;
            float accumulatedDelay = 0f;

            for (int i = 0; i < word.Length; i++)
            {
                char letter = word[i];
                var targetCell = targetCells[i];

                var flyingLetter = Instantiate(_flyingLetterPrefab, _flyingLettersLayer);
                flyingLetter.Setup(letter);
                flyingLetter.transform.position = _spawnPoint.position;

                flyingLetter.AnimateToTarget(
                    targetCell.GetWorldPosition(),
                    accumulatedDelay,
                    () =>
                    {
                        targetCell.RevealLetter(letter);
                    }
                );

                accumulatedDelay += delayPerLetter;
            }
        }
        
    }
}
