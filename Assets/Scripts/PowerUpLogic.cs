using UnityEngine;

public class PowerUpLogic : MonoBehaviour {

    public bool isExhausted = false;
    public Sprite exhaustedSprite = null;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Exhaust()
    {
        this.isExhausted = true;

        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = this.exhaustedSprite;
    }
}
