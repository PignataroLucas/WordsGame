using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace S.Gameplay.G.Grid
{
    public class WordRowView : MonoBehaviour
    {
        private List<LetterCellView> _cells = new();

        public bool IsEmpty => _cells.All(cell => string.IsNullOrEmpty(cell.Letter));

        public void Initialize(int letterCount, GameObject cellPrefab)
        {
            for(int i = 0; i < letterCount; i++)
            {
                var cellObj = Instantiate(cellPrefab, transform);
                var cell = cellObj.GetComponent<LetterCellView>();
                _cells.Add(cell);
            }
        }

        public void SetWord(string word)
        {
            for(int i = 0; i < word.Length; i++)
            {
                _cells[i].SetLetter(word[i]);
            }
        }

        public List<LetterCellView> GetCells()
        {
            return _cells;
        }
        
        public void ClearRow()
        {
            foreach (var cell in _cells)
            {
                cell.Clear();
            }
        }
        
        public bool MatchesLength(string word)
        {
            return _cells.Count == word.Length;
        }
    }
}
