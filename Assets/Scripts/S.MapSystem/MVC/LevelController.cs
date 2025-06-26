using S.Events.E.Transitions;
using Tests;
using UnityEngine;

namespace S.MapSystem.MVC
{
    public class LevelController
    {
        private LevelModel _levelModel;

        public LevelController()
        {
            _levelModel = new LevelModel();

            if(_levelModel == null)
            {
                Debug.LogError($"level model null");
            }
            
            EventBusContainer.Global.Subscribe<TestLevelEvent>(OnTestLevelEvent);
        }

        private void OnTestLevelEvent(TestLevelEvent evt)
        {
            _levelModel.LevelView.SetData(evt.LevelData);
            EventBusContainer.Global.Unsubscribe<TestLevelEvent>(OnTestLevelEvent);
        }
    }
}
