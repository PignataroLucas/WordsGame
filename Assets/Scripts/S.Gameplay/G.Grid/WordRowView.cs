using UnityEngine;

namespace S.Gameplay.G.Grid
{
    public class WordRowView : MonoBehaviour
    {
        public void AddLetterCell(LetterCellView cell)
        {
            cell.transform.SetParent(transform, false);
        }
    }
}
