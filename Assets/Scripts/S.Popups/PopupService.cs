using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace S.Popups
{
    public class PopupService 
    {
        private readonly Transform _popupParent;
        private const string PopupPath = "R.Popups/";
        
        private readonly Dictionary<string, Transform> _layerMap;
        private readonly Transform _defaultPopupLayer;

        public PopupService(Dictionary<string, Transform> layerMap, Transform defaultPopupLayer)
        {
            _layerMap = layerMap;
            _defaultPopupLayer = defaultPopupLayer;
        }

        public async Task ShowPopupAsync(string popupId)
        {
            var popup = await LoadAndInstantiatePopup(popupId);
            if(popup == null) return;

            await popup.ShowAsync();
        }

        public async Task ShowSequenceAsync(List<string> popupIds, float delayBetween = 0f)
        {
            PopupManager currentPopup = null;

            foreach (var id in popupIds)
            {
                currentPopup = await LoadAndInstantiatePopup(id);
                if(currentPopup == null) continue;

                await currentPopup.ShowAsync();

                if(delayBetween > 0)
                {
                    await Task.Delay((int)(delayBetween * 1000));
                }
            }

            // if (currentPopup != null)
            // {
            //     await currentPopup.CloseAsync();
            // }
        }

        private async Task<PopupManager> LoadAndInstantiatePopup(string popupId)
        {
            var prefab = Resources.Load<GameObject>($"{PopupPath}{popupId}");
            if(prefab == null)
            {
                Debug.LogError($"Popup prefab not found at path: {PopupPath}{popupId}");
                return null;
            }

            var instance = Object.Instantiate(prefab, _popupParent);

            var popup = instance.GetComponent<PopupManager>();

            string layerKey = popup.LayerKey;

            if (!string.IsNullOrEmpty(layerKey) && _layerMap.TryGetValue(layerKey, out var targetLayer))
            {
                instance.transform.SetParent(targetLayer, false);
            }
            else
            {
                instance.transform.SetParent(_defaultPopupLayer, false);
            }

            if(popup == null)
            {
                Debug.LogError($"Popup prefab {popupId} missing PopupManager component.");
                Object.Destroy(instance);
                return null;
            }

            await Task.Yield(); 
            return popup;
        }
    }
}
