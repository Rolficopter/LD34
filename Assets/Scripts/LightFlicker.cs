using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

    float mMax;

	// Use this for initialization
	void Start () {
        mMax = GetComponent<Light>().intensity;

        InvokeRepeating("Flicker", 0, .1f);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void Flicker()
    {
        GetComponent<Light>().intensity = Random.Range(20, 100) / 100f * mMax;
    }
}
