using TMPro;
using UnityEngine;

namespace S.Gameplay.G.Grid
{
    public class LetterCellView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _letterText;

        public void SetLetter(char letter)
        {
            _letterText.text = letter.ToString().ToUpper();
        }
    }
}
