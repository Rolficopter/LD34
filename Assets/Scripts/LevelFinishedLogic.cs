﻿using UnityEngine;
using System.Collections;
using System;

namespace Rolficopter.LD34.Assets.Scripts
{
    public class LevelFinishedLogic : MonoBehaviour
    {

        public UnityEngine.UI.Text deathText;
        public UnityEngine.UI.Text contText;

        // Use this for initialization
        void Start()
        {
            deathText.text = "You finished Level " + ApplicationModel.currentLevel + ".";

            if (Device.isMobileDevice())
            {
                contText.text = "Touch the Screen to continue.";
            }
            else if (Device.isControllerDevice())
            {
                contText.text = "Press A to continue.";
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Submit") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
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
            else if (Input.GetButtonDown("Cancel"))
            {
                Debug.Log("Exiting.");
                Application.Quit();
            }
        }
    }
}
