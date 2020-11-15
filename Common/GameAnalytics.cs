
namespace Match3Game.Common
{
    public class GameAnalytics
    {
        private static GameAnalytics instance;
        public static GameAnalytics Instance => instance is null ? instance = new GameAnalytics() : instance;

        public int Score { get; private set; }

        public void Reset()
        {
            Score = 0;
        }

        public void AddScore(int count)
        {
            Score += count;
        }
    }
}
