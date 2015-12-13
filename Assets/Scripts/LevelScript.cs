using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

    public int levelNumber = 1;

	// Use this for initialization
	void Start () {
        ApplicationModel.currentLevel = levelNumber;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
