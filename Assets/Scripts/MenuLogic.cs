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
            if (Input.GetButtonDown("Submit"))
            {
                this.StartGame();
            }
        }

        private void StartGame()
        {
            Debug.Log("Loading Level 1...");
            Application.LoadLevel(Constants.GetLevelName(Constants.Levels.Level1));
        }
    }
}
