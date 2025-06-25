using S.ScriptableObjects;
using Tests;

namespace S.Events.E.Transitions
{
    public class TestLevelEvent
    {
        public string Message;
        public LevelData LevelData;

        public TestLevelEvent(string message, LevelData data)
        {
            Message = message;
            LevelData = data;
        }
    }
}
