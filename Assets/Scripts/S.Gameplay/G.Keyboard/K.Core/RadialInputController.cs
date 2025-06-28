using System.Collections.Generic;
using Tests;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace S.Gameplay.G.Keyboard.K.Core
{
    public class RadialInputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    { 
        [SerializeField] private RadialKeyboardView _keyboardView;
        [SerializeField] private RectTransform _lineSegmentsContainer;
        [SerializeField] private GameObject _lineSegmentPrefab;
        [SerializeField] private WordGridController _wordGridController;

        private readonly List<LetterButtonView> _selectedLetters = new();

        private readonly List<GameObject> _activeSegments = new();

        public void OnPointerDown(PointerEventData eventData)
        {
            ClearSelection();
            TryAddLetter(eventData);
            UpdateLineSegments();
        }

        public void OnDrag(PointerEventData eventData)
        {
            TryAddLetter(eventData);
            UpdateLineSegments();

            
            string currentWord = BuildCurrentWord();
            Debug.Log($"[RadialInputController] Currently forming: {currentWord}");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(_selectedLetters.Count > 0)
            {
                string builtWord = BuildCurrentWord();
                Debug.Log($"[RadialInputController] Formed word: {builtWord}");
                
                ClearSelection();
                UpdateLineSegments();

            }
        }

        private void Update()
        {
            if(Touchscreen.current != null)
            {
                foreach(var touch in Touchscreen.current.touches)
                {
                    if(touch.press.wasReleasedThisFrame)
                    {
                        HandleGlobalPointerUp();
                        break;
                    }
                }
            }

            if(Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Debug.Log("[RadialInputController] Mouse Up Detected");
                HandleGlobalPointerUp();
            }
        }

        private void TryAddLetter(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach(var result in results)
            {
                var hitLetter = result.gameObject.GetComponent<LetterButtonView>();

                if (hitLetter == null)
                {
                    continue;
                }
                
                if(_selectedLetters.Count > 0 && _selectedLetters[_selectedLetters.Count - 1] == hitLetter)
                {
                    return;
                }

                if(_selectedLetters.Contains(hitLetter))
                {
                    Debug.Log("[RadialInputController] Backtrack detected → Clearing!");
                    ClearSelection();
                    return;
                }

                _selectedLetters.Add(hitLetter);
                hitLetter.Highlight();
                UpdateLineSegments();

                return; 
            }
        }

        private string BuildCurrentWord()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var letter in _selectedLetters)
            {
                sb.Append(letter.GetLetter());
            }
            return sb.ToString().ToUpper();
        }

        private LetterButtonView GetLetterUnderPointer(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (var result in results)
            {
                var letter = result.gameObject.GetComponent<LetterButtonView>();
                if (letter != null)
                {
                    return letter;
                }
            }

            return null;
         }
        

        private void HandleGlobalPointerUp()
        {
            if(_selectedLetters.Count > 0)
            {
                var builtWord = BuildCurrentWord();
                Debug.Log($"[RadialInputController] Final word formed: {builtWord}");

                var spawnPositions = new List<Vector3>();
                foreach(var letter in _selectedLetters)
                {
                    spawnPositions.Add(letter.GetRectTransform().position);
                }

                _wordGridController.TrySubmitWordFromRadial(builtWord, spawnPositions);

                ClearSelection();
                UpdateLineSegments();
            }
        }

        private void UpdateLineSegments()
        {
            ClearSegments();

            for (int i = 1; i < _selectedLetters.Count; i++)
            {
                var from = _selectedLetters[i - 1].GetRectTransform().position;
                var to = _selectedLetters[i].GetRectTransform().position;

                var segmentObj = Instantiate(_lineSegmentPrefab, _lineSegmentsContainer);
                var segmentRect = segmentObj.GetComponent<RectTransform>();

                PositionSegmentBetween(segmentRect, from, to);

                _activeSegments.Add(segmentObj);
            }
        }

        private void PositionSegmentBetween(RectTransform segment, Vector3 start, Vector3 end)
        {
            Vector3 direction = end - start;
            float distance = direction.magnitude;

            segment.sizeDelta = new Vector2(distance, segment.sizeDelta.y);
            segment.pivot = new Vector2(0, 0.5f);
            segment.position = start;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            segment.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void ClearSelection()
        {
            foreach (var letter in _selectedLetters)
            {
                letter.ClearHighlight();
            }
            _selectedLetters.Clear();
            ClearSegments();
        }

        private void ClearSegments()
        {
            foreach (var segment in _activeSegments)
            {
                Destroy(segment);
            }
            _activeSegments.Clear();
        }
        
    }
}
