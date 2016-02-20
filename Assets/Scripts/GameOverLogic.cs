﻿using UnityEngine;
using System.Collections;

namespace Rolficopter.LD34.Assets.Scripts
{
    public class GameOverLogic : MonoBehaviour
    {

        public UnityEngine.UI.Text deathText;
        public UnityEngine.UI.Text restartText;

        // Use this for initialization
        void Start()
        {
            deathText.text = "You died in Level " + ApplicationModel.currentLevel + ".";

            if (Device.isMobileDevice())
            {
                restartText.text = "Touch the Screen to Restart.";
            }
            else if (Device.isControllerDevice())
            {
                restartText.text = "Press A to Restart.";
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Submit") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                this.LoadLastLevel();
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                Debug.Log("Exiting.");
                Application.Quit();
            }
        }

        private void LoadLastLevel()
        {
            Application.LoadLevel(Constants.GetLevelName(ApplicationModel.loadedLevel));
        }
    }
}