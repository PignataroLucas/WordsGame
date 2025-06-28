using System.Collections.Generic;
using DG.Tweening;
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
            
            AnimateShow();
        }

        private void AnimateShow()
        {
            var containerRect = GetComponent<RectTransform>();
            containerRect.localScale = Vector3.zero;
            containerRect.DOScale(1f, 0.3f).SetEase(Ease.OutBack);

            var baseDelay = 0.1f;
            var delayStep = 0.05f;

            for(int i = 0; i < _spawnedButtons.Count; i++)
            {
                var btnRect = _spawnedButtons[i].GetRectTransform();
                btnRect.localScale = Vector3.zero;

                btnRect.DOScale(1f, 0.3f)
                    .SetEase(Ease.OutBack)
                    .SetDelay(baseDelay + i * delayStep);
            }
        }
        
        
        
    }
}
