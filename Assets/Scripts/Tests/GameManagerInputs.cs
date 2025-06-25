using S.Events.E.Transitions;
using S.MapSystem.MVC;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tests
{
    public class GameManagerInputs : MonoBehaviour
    {
        private void Start()
        {
            var controller = new LevelController();

            if(controller != null)
            {
                Debug.Log($"Correct Initialization");
            }
        }

        private void Update()
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                EventBusContainer.Global.Publish(new TestLevelEvent("Testing EventBuss"));
            }
        }
    }
}
