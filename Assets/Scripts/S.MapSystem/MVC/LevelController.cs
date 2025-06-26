using S.Events.E.Transitions;
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
            });
        }

        private void OnTestLevelEvent(TestLevelEvent evt)
        {
            if(_levelView != null)
            { 
                _levelView.SetData(evt.LevelData);
            }
            //EventBusContainer.Global.Unsubscribe<TestLevelEvent>(OnTestLevelEvent);
        }
    }
}
