using System;

namespace Rolficopter.LD34.Assets.Scripts
{
    abstract class Constants
    {
        public enum Levels
        {
            Menu,
            Level1
        }

        public static String GetLevelName(Levels level)
        {
            return String.Format("{0}", level.ToString());
        }
    }
}
