using S.ScriptableObjects;

namespace S.MapSystem.MVC
{
    public class LevelModel
    {
        private LevelData _levelData;
        private LevelView _levelView; 
        //private UtilityHeaderData _utilityHeaderData;

        public LevelData LevelData => _levelData;
        public LevelView LevelView => _levelView;
    }
}
