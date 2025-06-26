using S.ScriptableObjects;
using UnityEngine;

namespace S.MapSystem.MVC
{
    public class LevelView : MonoBehaviour
    {
        public void SetData(LevelData evtLevelData)
        {
            Debug.Log($"[LevelController] Level Data information : {evtLevelData.LevelId}");
        }
    }
}
