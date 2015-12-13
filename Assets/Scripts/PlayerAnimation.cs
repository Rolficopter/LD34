using UnityEngine;
using System.Collections;

namespace Rolficopter.LD34.Assets.Scripts
{
    public class PlayerAnimation : MonoBehaviour
    {

        public Sprite[] animationSprites;

        int currentSpriteIndex = 0;
        SpriteRenderer mSpriteRenderer;

        // Use this for initialization
        void Start()
        {
            mSpriteRenderer = GetComponent<SpriteRenderer>();

            InvokeRepeating("Animate", 0, .075f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Animate()
        {
            mSpriteRenderer.sprite = this.animationSprites[currentSpriteIndex];

            currentSpriteIndex = (currentSpriteIndex + 1) % animationSprites.Length;
        }
    }
}