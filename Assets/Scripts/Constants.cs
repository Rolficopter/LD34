using System;

namespace Rolficopter.LD34.Assets.Scripts
{
    public abstract class Constants
    {
        public enum Levels
        {
            Menu,
            Level1,
            Level2,
            Level3,
            Level4,
            Level5,
            Level6,
            LevelFinished,
            GameOver
        }

        public static String GetLevelName(Levels level)
        {
            return String.Format("{0}", level.ToString());
        }
    }
}
