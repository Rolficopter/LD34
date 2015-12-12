using UnityEngine;
using System.Collections;

public class SpikesLogic : MonoBehaviour {

    public GameObject player = null;

    private Collider2D playerCollider = null;
    private Collider2D[] spikeColliders = null;

	// Use this for initialization
	void Start () {
        this.playerCollider = this.player.GetComponent<Collider2D>();

        this.spikeColliders = new Collider2D[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.spikeColliders[i] = this.transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if ( this.IsPlayerTouchingSpikes() )
        {
            PlayerControl player = this.player.GetComponent<PlayerControl>();
            if ( player != null )
            {
                player.Die();
            }
        }
	}

    private bool IsPlayerTouchingSpikes()
    {
        foreach (var spikeCollider in this.spikeColliders)
        {
            if ( this.playerCollider.IsTouching(spikeCollider) )
            {
                return true;
            }
        }

        return false;
    }
}
