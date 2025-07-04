using System.Threading.Tasks;
using S.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace S.Popups
{
    public class RewardPopup : PopupManager
    {
        [SerializeField] private TextMeshProUGUI _text;

        public override void Prepare(LevelData levelData)
        {
            base.Prepare(levelData);
            _text.text = $"CLAIM REWARD : {levelData.RewardCoinsAmount} COINS";
        }

        protected override async Task CloseAsync()
        {
            await base.CloseAsync();
            //Agregar monedas al usuario
        }
    }
}
