using System;
using S.MapSystem.MVC;
using S.Popups;
using S.Utility.U.EventBus;
using UnityEngine;
using S.Utility.U.ServiceLocator;


namespace S.Utility.U.GameInitializer
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private Transform _popupLayer;
        [SerializeField] private LevelView _levelView;

        private void Awake()
        {
            Services.Add<IEventBus>(new EventBus.EventBus());
            
            var popupService = new PopupService(_popupLayer);
            Services.Add<PopupService>(popupService);

            var levelController = new LevelController(_levelView);
        }
    }
}
