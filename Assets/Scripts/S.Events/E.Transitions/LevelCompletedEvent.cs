using S.ScriptableObjects;

namespace S.Events.E.Transitions
{
    public class LevelCompletedEvent
    {
        public LevelData LevelData;
        
        public LevelCompletedEvent(LevelData data)
        {
            LevelData = data;
        }
    }
}
