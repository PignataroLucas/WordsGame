using System;
using S.MapSystem.MVC;
using S.Utility.U.EventBus;
using UnityEngine;
using S.Utility.U.ServiceLocator;


namespace S.Utility.U.GameInitializer
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private LevelView _levelView;

        private void Awake()
        {
            Services.Add<IEventBus>(new EventBus.EventBus());

            var levelController = new LevelController(_levelView);
        }
    }
}
