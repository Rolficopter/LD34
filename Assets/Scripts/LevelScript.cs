using UnityEngine;
using System.Collections;

namespace Rolficopter.LD34.Assets.Scripts
{
    public class LevelScript : MonoBehaviour
    {

        public int levelNumber = 1;
        public Constants.Levels levelID = Constants.Levels.Menu;

        // Use this for initialization
        void Start()
        {
            ApplicationModel.currentLevel = levelNumber;
            ApplicationModel.loadedLevel = levelID;
        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}
