using System.Collections.Generic;
using UnityEngine;

namespace S.Gameplay.G.Keyboard.K.Core
{
    public class RadialKeyboardView : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private LetterButtonView _letterButtonPrefab;
        [SerializeField] private float _radius = 200f;
        
        private List<LetterButtonView> _spawnedButtons = new();

        
        public void BuildKeyboard(List<char> letters)
        {
            foreach (var btn in _spawnedButtons)
            {
                Destroy(btn.gameObject);
            }
            _spawnedButtons.Clear();

            var angleStep = 360f / letters.Count;
            var currentAngle = 0f;

            for(var i = 0; i < letters.Count; i++)
            {
                var letterObj = Instantiate(_letterButtonPrefab, _container);
                letterObj.SetLetter(letters[i]);

                float rad = currentAngle * Mathf.Deg2Rad;
                float x = _radius * Mathf.Cos(rad);
                float y = _radius * Mathf.Sin(rad);

                letterObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

                _spawnedButtons.Add(letterObj);

                currentAngle += angleStep;
            }
        }
    }
}
