using System;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour {

    public bool isExhausted = false;
    public Sprite exhaustedSprite = null;

    Light mHalo;
    float mInitialRange;

	// Use this for initialization
	void Start () {
        mHalo = GetComponent<Light>();
        mInitialRange = mHalo.range;
	}
	
	// Update is called once per frame
	void Update () {
        if (!this.isExhausted)
        {
            this.mHalo.range = mInitialRange * (float) Math.Abs(Math.Sin(Time.realtimeSinceStartup * 2));
        }
	}

    public void Exhaust()
    {
        this.isExhausted = true;

        this.mHalo.enabled = false;

        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = this.exhaustedSprite;
    }
}
