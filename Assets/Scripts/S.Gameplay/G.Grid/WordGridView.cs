using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace S.Gameplay.G.Grid
{
    public class WordGridView : MonoBehaviour
    {
        [SerializeField] private Transform _gridParent;
        
        private readonly List<WordRowView> _rows = new();
        private readonly Dictionary<string, WordRowView> _wordToRowMap = new();

        public void BuildGrid(List<string> words, GameObject rowPrefab, GameObject cellPrefab)
        {
            foreach(Transform child in _gridParent)
            { 
                Destroy(child.gameObject);
            }

            _rows.Clear();
            _wordToRowMap.Clear();

            words.Sort(CompareWords);

            var baseDelay = 0f;
            var delayIncrement = 0.05f;

            foreach(var word in words)
            {
                var rowObj = Instantiate(rowPrefab, _gridParent);
                var row = rowObj.GetComponent<WordRowView>();
                row.Initialize(word.Length, cellPrefab);

                _rows.Add(row);
                _wordToRowMap[word] = row;

                foreach(var cell in row.GetCells())
                {
                    cell.PlayAppearAnimation(baseDelay);
                    baseDelay += delayIncrement;
                }
            }
        }

        public void ClearPrevious()
        {
            foreach (var row in _rows)
            {
                row.ClearRow();
            }
        }

        public bool IsGridComplete()
        {
            return _rows.All(row => row.IsCompleted);
        }

        private int CompareWords(string a, string b)
        {
            var lengthCompare = a.Length.CompareTo(b.Length);
            if(lengthCompare != 0)
            {
                return lengthCompare;
            }
            
            return string.Compare(a, b, System.StringComparison.Ordinal);
        }

        public WordRowView GetRowForWord(string word)
        {
            return _wordToRowMap.TryGetValue(word, out var row) ? row : null;
        }

        public WordRowView GetNextEmptyRow(int wordLength)
        {
            foreach (var row in _rows)
            {
                if(row.IsEmpty && row.MatchesLength(wordLength))
                {
                    return row;
                }
            }
            return null;
        }

        public void FillNextEmpty(string word)
        {
            foreach(var row in _rows)
            {
                if(row.IsEmpty)
                {
                    row.SetWord(word);
                    return;
                }
            }
        }
        
        public bool TrySetWord(string word)
        {
            foreach(var row in _rows)
            {
                if(row.IsEmpty && row.MatchesLength(word))
                {
                    row.SetWord(word);
                    return true;
                }
            }

            return false;
        }
        
    }
}
