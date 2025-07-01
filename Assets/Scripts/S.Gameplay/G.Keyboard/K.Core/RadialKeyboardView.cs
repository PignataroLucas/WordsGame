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
        private List<char> _currentLetters = new();

        public void BuildKeyboard(List<char> letters)
        {
            _currentLetters = new List<char>(letters);

            ClearButtons();

            ArrangeLettersRadially(_currentLetters);
            AnimateShow();
        }

        private void ClearButtons()
        {
            foreach (var btn in _spawnedButtons)
            {
                Destroy(btn.gameObject);
            }
            _spawnedButtons.Clear();
        }

        private void ArrangeLettersRadially(List<char> letters)
        {
            float angleStep = 360f / letters.Count;
            float currentAngle = 0f;

            foreach (var letter in letters)
            {
                var letterObj = Instantiate(_letterButtonPrefab, _container);
                letterObj.SetLetter(letter);

                var rad = currentAngle * Mathf.Deg2Rad;
                var x = _radius * Mathf.Cos(rad);
                var y = _radius * Mathf.Sin(rad);

                letterObj.GetRectTransform().anchoredPosition = new Vector2(x, y);

                _spawnedButtons.Add(letterObj);

                currentAngle += angleStep;
            }
        }

        public void ShuffleLettersWithAnimation()
        {
            ShuffleList(_currentLetters);

            var targetPositions = CalculateRadialPositions(_currentLetters.Count);

            var animationDuration = 0.5f;
            Ease easing = Ease.OutBack;

            for(var i = 0; i < _spawnedButtons.Count; i++)
            {
                var letterBtn = _spawnedButtons[i];

                letterBtn.SetLetter(_currentLetters[i]);

                letterBtn.GetRectTransform()
                    .DOAnchorPos(targetPositions[i], animationDuration)
                    .SetEase(easing);
            }
        }

        private List<Vector2> CalculateRadialPositions(int count)
        {
            var positions = new List<Vector2>();
            var angleStep = 360f / count;
            var currentAngle = 0f;

            for (int i = 0; i < count; i++)
            {
                var rad = currentAngle * Mathf.Deg2Rad;
                var x = _radius * Mathf.Cos(rad);
                var y = _radius * Mathf.Sin(rad);

                positions.Add(new Vector2(x, y));
                currentAngle += angleStep;
            }

            return positions;
        }

        private void ShuffleList(List<char> list)
        {
            System.Random rng = new System.Random();
            var n = list.Count;

            while(n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
        
        private void AnimateShow()
        {
            var containerRect = GetComponent<RectTransform>();
            containerRect.localScale = Vector3.zero;
            containerRect.DOScale(1f, 0.3f).SetEase(Ease.OutBack);

            float baseDelay = 0.1f;
            float delayStep = 0.05f;

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
