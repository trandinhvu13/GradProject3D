namespace Game
{
    public class PlayerDataManager : MonoSingleton<PlayerDataManager>
    {
        public int levelIDToLoad;
        public int numberOfLevel;
        public int currentLevel;
        public int currentStar;

        protected override void InternalInit()
        {
        }

        protected override void InternalOnDestroy()
        {
        }

        protected override void InternalOnDisable()
        {
        }

        protected override void InternalOnEnable()
        {
        }

        public int GetCurrentStar()
        {
            return currentStar;
        }

        public int GetTotalStar()
        {
            return 3 * numberOfLevel;
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }
    }
}