using UnityEngine;
using System.Collections;

public class CameraSideFollow : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, target.transform.position.x, 1f * Time.deltaTime),
            transform.position.y,
            transform.position.z);
	}
}
