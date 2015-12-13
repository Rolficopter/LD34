using UnityEngine;
using System.Collections;
using System;

namespace Rolficopter.LD34.Assets.Scripts
{
    public class LevelFinishedLogic : MonoBehaviour
    {

        public UnityEngine.UI.Text deathText;

        // Use this for initialization
        void Start()
        {
            deathText.text = "You finished Level " + ApplicationModel.currentLevel + ".";
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                int nextLevel = ApplicationModel.currentLevel + 1;
                string nextLevelName = "Level" + nextLevel;
                if (Application.CanStreamedLevelBeLoaded(nextLevelName))
                {
                    Application.LoadLevel(nextLevelName);
                }
                else
                {
                    Application.LoadLevel(Constants.GetLevelName(Constants.Levels.Menu));
                }
                
            }
        }
    }
}
