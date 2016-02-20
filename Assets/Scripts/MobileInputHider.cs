using UnityEngine;
using System.Collections;

public class MobileInputHider : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!Device.isMobileDevice() || Device.isAndroidTv()) {
            this.GetComponent<Canvas>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
