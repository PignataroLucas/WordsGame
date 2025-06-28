using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace S.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Map/Level Data")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int levelId;  
        [SerializeField] private List<string> wordList;
        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private AudioClip music;

        public int LevelId => levelId;
        public List<string> WordList => wordList;
        public Sprite BackgroundImage => backgroundImage;
        public AudioClip Music => music;
        
        public List<char> GetUniqueLetters()
        {
            return wordList
                .SelectMany(w => w.ToLower().ToCharArray())
                .Distinct()
                .ToList();
        }
    }
}
