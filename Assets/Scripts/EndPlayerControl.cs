﻿using UnityEngine;
using System.Collections;

public class EndPlayerControl : MonoBehaviour {

    Rigidbody2D mRigidBody;
    SpriteRenderer mSprite;
    public SpriteRenderer mAnimationSprite;

    bool mWalking = true;
    bool mDissolving = false;

	// Use this for initialization
	void Start () {
        mRigidBody = GetComponent<Rigidbody2D>();
        mSprite = GetComponent<SpriteRenderer>();

        Invoke("Stop", 20);
	}
	
	// Update is called once per frame
	void Update () {
        if(mWalking)
            mRigidBody.velocity = new Vector2(3, mRigidBody.velocity.y);

        if (mDissolving)
        {
            mSprite.color = Vector4.Lerp(mSprite.color, new Vector4(1, 1, 1, 0), Time.deltaTime);
            mAnimationSprite.color = mSprite.color;

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 6, transform.position.z), Time.deltaTime * 0.5f);
        }
	}

    void Stop()
    {
        mWalking = false;
        Invoke("Dissolve", 2);
    }

    void Dissolve()
    {
        mDissolving = true;
        mRigidBody.isKinematic = true;
        Invoke("Finish", 5);
    }

    void Finish()
    {
        Application.LoadLevel(Rolficopter.LD34.Assets.Scripts.Constants.Levels.Menu.ToString());
    }
}
