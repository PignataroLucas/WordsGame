using System.Collections.Generic;
using S.ScriptableObjects;
using UnityEngine;

namespace Tests
{
    [CreateAssetMenu(menuName = "Map/Level Database")]
    public class LevelDatabase : ScriptableObject
    {
        [SerializeField] private List<LevelData> allLevels;

        public LevelData GetLevelById(int levelId)
        {
            return allLevels.Find(level => level.LevelId == levelId);
        }
    }
}
