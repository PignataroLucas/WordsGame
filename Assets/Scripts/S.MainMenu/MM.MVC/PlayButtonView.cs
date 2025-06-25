using System;
using S.Events.E.Transitions;
using S.ScriptableObjects;
using Tests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace S.MainMenu.MM.MVC
{
    public class PlayButtonView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private UserStateData _userStateData;
        [SerializeField] private LevelDatabase _levelDatabase;

        private LevelData _currentLevelData;

        private void Start()
        {
            int currentLevelId = _userStateData.CurrentGameLevel;
            _currentLevelData = _levelDatabase.GetLevelById(currentLevelId);
            
            _label.text = $"Play Level {_currentLevelData.LevelId}";

            _playButton.onClick.AddListener(() =>
            {
                EventBusContainer.Global.Publish(new TestLevelEvent("Jugar nivel actual", _currentLevelData));
            });
            
        }
    }
}
