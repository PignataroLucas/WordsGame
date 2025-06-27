using System.Collections.Generic;
using S.Gameplay.G.Grid;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tests
{
    public class WordGridController : MonoBehaviour
    {
        [SerializeField] private WordGridView _wordGridView;
        private List<string> _validWords;

        private string _testWordValid = "cats";   
        private string _testWordInvalid = "dogs"; 

        public void SetLevelWords(List<string> wordList)
        {
            _validWords = wordList;
        }

        private void Update()
        {
            if(Keyboard.current.aKey.wasPressedThisFrame)
            { 
                TrySubmitWord(_testWordValid);
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
                _wordGridView.FillNextEmpty(word); 
            }
            else
            {
                Debug.Log($"❌ Wrong word : {word}");
            }
        }
    }
}
