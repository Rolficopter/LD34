using UnityEngine;
using System.Collections;

public class FinishLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Light>().intensity = Random.Range(50, 100) / 100f;
	}


}
