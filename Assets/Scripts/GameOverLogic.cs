using UnityEngine;
using System.Collections;

namespace Rolficopter.LD34.Assets.Scripts
{
    public class GameOverLogic : MonoBehaviour
    {

        public UnityEngine.UI.Text deathText;

        // Use this for initialization
        void Start()
        {
            deathText.text = "You died in Level " + ApplicationModel.currentLevel + ".";
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                this.LoadLastLevel();
            }
        }

        private void LoadLastLevel()
        {
            Application.LoadLevel(Constants.GetLevelName(ApplicationModel.loadedLevel));
        }
    }
}