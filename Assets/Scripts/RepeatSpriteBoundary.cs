using UnityEngine;
using System.Collections;


// Generates a nice set of repeated sprites inside a streched sprite renderer
// @NOTE Horizontal only, you can easily expand this to v with a little tweaking
namespace Rolficopter.LD34.Assets.Scripts
{
    // @NOTE the attached sprite's position should be "top left" or the children will not align properly
    // Strech out the image as you need in the sprite render, the following script will auto-correct it
    [RequireComponent(typeof(SpriteRenderer))]
    public class RepeatSpriteBoundary : MonoBehaviour
    {
        SpriteRenderer sprite;
    
        public Sprite secondarySprite;

        void Awake()
        {
            // Get the current sprite with an unscaled size
            sprite = GetComponent<SpriteRenderer>();
            Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);

            // Generate a child prefab of the sprite renderer
            GameObject childPrefab = new GameObject();
            SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
            childPrefab.transform.position = transform.position;
            if (secondarySprite != null && Random.Range(0, 2) == 1)
            {
                childSprite.sprite = secondarySprite;
            } 
            else
            {
                childSprite.sprite = sprite.sprite;
            }
            childSprite.sortingOrder = sprite.sortingOrder;
            childSprite.material = sprite.material;

            // Loop through and spit out repeated tiles
            GameObject child;
            for (int i = 1, l = (int)Mathf.Round(sprite.bounds.size.x); i < l; i++)
            {
                child = Instantiate(childPrefab) as GameObject;
                child.transform.position = transform.position + (new Vector3(spriteSize.x, 0, 0) * i);
                child.transform.parent = transform;
                child.transform.localScale = new Vector3(child.transform.localScale.x, 1, 1);
            }

            // Set the parent last on the prefab to prevent transform displacement
            childPrefab.transform.parent = transform;
            childPrefab.transform.localScale = new Vector3(childPrefab.transform.localScale.x, 1, 1);

            // Disable the currently existing sprite component since its now a repeated image
            sprite.enabled = false;
        }
    }
    }