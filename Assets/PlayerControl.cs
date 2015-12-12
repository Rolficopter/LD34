﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    Rigidbody2D mRigidBody;
    private float flightSpeed = 5;

	// Use this for initialization
	void Start () {
        this.mRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            mRigidBody.AddForce(Vector2.up * 1000);
        }
	}
}
