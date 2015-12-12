using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    Rigidbody2D mRigidBody;
    Transform mTransform;
    public float jumpFactor = 200;
    public float growingFactor = 1.01f;

	// Use this for initialization
	void Start () {
        this.mRigidBody = GetComponent<Rigidbody2D>();
        this.mTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            mRigidBody.AddForce(Vector2.up * jumpFactor);
            mTransform.localScale = new Vector3(mTransform.localScale.x * growingFactor, mTransform.localScale.x * growingFactor, 1);
        }
	}
}
