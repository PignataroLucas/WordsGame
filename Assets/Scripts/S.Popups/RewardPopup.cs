using System.Threading.Tasks;
using UnityEngine;

namespace S.Popups
{
    public class RewardPopup : PopupManager
    {
        public override async Task ShowAsync()
        {
            await base.ShowAsync();
        }

        public override async Task CloseAsync()
        {
            await base.CloseAsync();
        }
    }
}
