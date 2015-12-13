﻿using System;
using UnityEngine;

namespace Rolficopter.LD34.Assets.Scripts
{
    public class PowerUpLogic : MonoBehaviour
    {

        public bool isExhausted = false;
        public Sprite exhaustedSprite = null;

        Light mHalo;
        float mInitialRange;

        AudioSource mAudioSource;

        ParticleSystem mParticleSystem;

        // Use this for initialization
        void Start()
        {
            mHalo = GetComponent<Light>();
            mInitialRange = mHalo.range;
            mAudioSource = GetComponent<AudioSource>();
            mParticleSystem = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!this.isExhausted)
            {
                this.mHalo.range = mInitialRange * (float)Math.Abs(Math.Sin(Time.realtimeSinceStartup * 2));
                this.mAudioSource.pitch = (float)Math.Abs(Math.Sin(Time.realtimeSinceStartup)) + 1;
            }
        }

        public void Exhaust()
        {
            this.isExhausted = true;

            this.mHalo.enabled = false;
            mAudioSource.enabled = false;
            this.mParticleSystem.enableEmission = false;

            SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = this.exhaustedSprite;
        }
    }
}