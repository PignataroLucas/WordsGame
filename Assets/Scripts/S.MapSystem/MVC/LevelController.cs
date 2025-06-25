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
            Debug.Log($"[LevelController] event received: {evt.Message}");
            Debug.Log($"[LevelController] Level Data information : {evt.LevelData.LevelId}");
            EventBusContainer.Global.Unsubscribe<TestLevelEvent>(OnTestLevelEvent);
        }
    }
}
