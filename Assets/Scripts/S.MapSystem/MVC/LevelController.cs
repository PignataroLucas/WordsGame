using System.Collections.Generic;
using S.Events.E.Transitions;
using S.Popups;
using S.Utility.U.EventBus;
using S.Utility.U.ServiceLocator;
using UnityEngine;

namespace S.MapSystem.MVC
{
    public class LevelController
    {
        private readonly LevelView _levelView;

        public LevelController(LevelView levelView)
        {
            _levelView = levelView;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            Services.WaitFor<IEventBus>(eventBus =>
            {
                eventBus.Subscribe<TestLevelEvent>(OnTestLevelEvent);
                eventBus.Subscribe<LevelCompletedEvent>(OnLevelCompleted);
                eventBus.Subscribe<ReturnToMenuEvent>(OnReturnToMenu);
            });
        }

        private void OnReturnToMenu(ReturnToMenuEvent obj)
        {
            if (_levelView != null)
            {
                _levelView.AnimateOutToMenu(() =>
                {
                    Debug.Log("[LevelController] Volviendo al menú!");
                });
            }
            
        }

        private void OnLevelCompleted(LevelCompletedEvent obj)
        {
            Services.WaitFor<PopupService>(async popupService =>
            {
                await popupService.ShowPopupAsync("RewardPopup", obj.LevelData);
            });
            
        }

        private void OnTestLevelEvent(TestLevelEvent evt)
        {
            if(_levelView != null)
            { 
                _levelView.SetData(evt.LevelData);
            }
        }
    }
}
