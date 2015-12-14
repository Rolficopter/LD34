using System.Threading;
using UnityEngine;

namespace Rolficopter.LD34.Assets.Scripts
{
    public class MenuLogic : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Submit") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                this.StartGame();
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                Debug.Log("Exiting.");
                Application.Quit();
            }
        }

        private bool CanStartGame()
        {
            return Application.CanStreamedLevelBeLoaded(Constants.GetLevelName(Constants.Levels.Level1));
        }
        private void StartGame()
        {
            if ( this.CanStartGame())
            {
                Debug.Log("Loading Level 1...");
                Application.LoadLevel(Constants.GetLevelName(Constants.Levels.Level1));
            }
            else
            {
                Debug.Log("Level 1 not yet loaded.");
            }
        }
    }
}
