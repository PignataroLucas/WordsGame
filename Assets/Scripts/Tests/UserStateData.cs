using S.ScriptableObjects;
using UnityEngine;

namespace Tests
{
    [CreateAssetMenu(menuName = "Game/User State Data")]
    public class UserStateData : ScriptableObject
    {
        [SerializeField] private int  currentGameLevel;

        public int CurrentGameLevel
        {
            get => currentGameLevel;
            set => currentGameLevel = value;
        }
    }
}
