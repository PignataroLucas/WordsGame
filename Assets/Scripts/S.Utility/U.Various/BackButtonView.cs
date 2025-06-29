using S.Events.E.Transitions;
using S.Utility.U.EventBus;
using S.Utility.U.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace S.Utility.U.Various
{
    public class BackButtonView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        private void Start()
        {
            _backButton.onClick.AddListener(OnBackClicked);
        }

        private void OnBackClicked()
        {
            Services.WaitFor<IEventBus>(bus =>
            {
                bus.Publish(new ReturnToMenuEvent());
            });
        }
    }
}
