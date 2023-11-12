namespace Minesweeper.Scenes.Gameplay
{
    public class Settings
    {
        private static Settings _instance;

        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        public int NumberOfMines { get; private set; }
        public int NumberOfLives { get; private set; }

        private Settings() { }

        public static Settings GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            SetSettings(Difficulty.Easy);

            return _instance;
        }

        public static void SetSettings(Difficulty difficulty)
        {
            if (difficulty == Difficulty.Easy)
            {
                SetSettings(9, 9, 10, 1);
            }
            else if (difficulty == Difficulty.Medium)
            {
                SetSettings(16, 16, 40, 1);
            }
            else if (difficulty == Difficulty.Expert)
            {
                SetSettings(16, 30, 99, 1);
            }
            else
            {
                throw new System.ArgumentException($"{difficulty} is not a valid difficulty");
            }
        }

        public static void SetSettings(int width, int height, int mines, int lives)
        {
            _instance = new Settings
            {
                GridWidth = width,
                GridHeight = height,
                NumberOfMines = mines,
                NumberOfLives = lives,
            };
        }
    }
}
