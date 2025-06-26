using System.Collections.Generic;
using UnityEngine;

namespace S.Gameplay.G.Grid
{
    public class WordGridView : MonoBehaviour
    {
        [SerializeField] private WordRowView _wordRowPrefab;
        [SerializeField] private LetterCellView _letterCellPrefab;
        [SerializeField] private Transform _gridParent;

        public void BuildGrid(List<string> wordList)
        {
            foreach (Transform child in _gridParent)
            {
                Destroy(child.gameObject);
            }

            foreach (var word in wordList)
            {
                var row = Instantiate(_wordRowPrefab, _gridParent);
                foreach (var letter in word)
                {
                    var cell = Instantiate(_letterCellPrefab);
                    cell.SetLetter(letter);
                    row.AddLetterCell(cell);
                }
            }
        }
    }
}
