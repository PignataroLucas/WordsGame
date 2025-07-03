using System;
using System.Collections.Generic;
using S.MapSystem.MVC;
using S.Popups;
using S.Utility.U.EventBus;
using UnityEngine;
using S.Utility.U.ServiceLocator;


namespace S.Utility.U.GameInitializer
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private Transform defaultPopupLayer, rewardPopupLayer;
        [SerializeField] private LevelView _levelView;

        private void Awake()
        {
            var popupLayers = new Dictionary<string, Transform>
            {
                { "Default", defaultPopupLayer },
                { "Reward", rewardPopupLayer },
            };

            var popupService = new PopupService(popupLayers, defaultPopupLayer);
            Services.Add<PopupService>(popupService);
            
            Services.Add<IEventBus>(new EventBus.EventBus());
            
            var levelController = new LevelController(_levelView);
        }
    }
}
