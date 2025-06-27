using System.Collections.Generic;
using UnityEngine;

namespace S.Gameplay.G.Grid
{
    public class WordGridView : MonoBehaviour
    {
        [SerializeField] private Transform _gridParent;
        private readonly List<WordRowView> _rows = new();

        public void BuildGrid(List<string> words, GameObject rowPrefab, GameObject cellPrefab)
        {
            // foreach (var word in words)
            // {
            //     var rowObj = Instantiate(rowPrefab, _gridParent);
            //     var row = rowObj.GetComponent<WordRowView>();
            //
            //     row.Initialize(word.Length, cellPrefab);
            //     _rows.Add(row);
            // }
            // Limpiar grilla anterior
            // foreach (Transform child in _gridParent)
            // {
            //     Destroy(child.gameObject);
            // }
            //
            // float baseDelay = 0f;
            // float delayIncrement = 0.05f;
            //
            // foreach (var word in words)
            // {
            //     var rowObj = Instantiate(rowPrefab, _gridParent);
            //     var row = rowObj.GetComponent<WordRowView>();
            //
            //     row.Initialize(word.Length, cellPrefab);
            //
            //     var cells = rowObj.GetComponentsInChildren<LetterCellView>();
            //
            //     foreach (var cell in cells)
            //     {
            //         cell.PlayAppearAnimation(baseDelay);
            //         baseDelay += delayIncrement;
            //     }
            // }
            
            foreach (Transform child in _gridParent)
            {
                Destroy(child.gameObject);
            }

            _rows.Clear(); 

            float baseDelay = 0f;
            float delayIncrement = 0.05f;

            foreach (var word in words)
            {
                var rowObj = Instantiate(rowPrefab, _gridParent);
                var row = rowObj.GetComponent<WordRowView>();
                row.Initialize(word.Length, cellPrefab);
                _rows.Add(row);

                foreach (var cell in row.GetCells())
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
        
        public WordRowView GetNextEmptyRow(int wordLength)
        {
            foreach (var row in _rows)
            {
                if (row.IsEmpty && row.MatchesLength(wordLength))
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
