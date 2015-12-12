using UnityEngine;

public class PlayerControl : MonoBehaviour {

    Rigidbody2D mRigidBody;
    Transform mTransform;

    public Transform groundCheck;
    public LayerMask groundMask;

    public float jumpFactor = 200;
    public float growingFactor = 1.01f;

	// Use this for initialization
	void Start () {
        this.mRigidBody = GetComponent<Rigidbody2D>();
        this.mTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") && groundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(groundMask))
        {
            mRigidBody.AddForce(Vector2.up * jumpFactor);
            this.Grow();
        }

        bool isPressingDown = Input.GetAxis("Vertical") < 0;
        if ( isPressingDown )
        {
            // check for down
            GameObject powerUp = this.GetCollidingPowerUp();
            if ( powerUp != null )
            {
                PowerUpLogic powerUpLogic = powerUp.GetComponent<PowerUpLogic>();

                if (!powerUpLogic.isExhausted)
                {
                    powerUpLogic.Exhaust();

                    this.Shrink();
                }
            }
        }
	}

    private GameObject GetCollidingPowerUp()
    {
        GameObject powerUpsNode = GameObject.Find("PowerUps");
        if (powerUpsNode != null)
        {
            BoxCollider2D playerCollider = this.gameObject.GetComponent<BoxCollider2D>();

            foreach (var powerUpObject in powerUpsNode.transform)
            {
                GameObject powerUp = ((Transform)powerUpObject).gameObject;
                BoxCollider2D powerUpCollider = powerUp.GetComponent<BoxCollider2D>();
                if ( playerCollider.IsTouching(powerUpCollider) )
                {
                    return powerUp;
                }
            }
        }

        return null;
    }

    private void Grow()
    {
        Debug.Log("Growing player...");
        mTransform.localScale = new Vector3(mTransform.localScale.x * growingFactor, mTransform.localScale.x * growingFactor, 1);
    }

    private void Shrink()
    {
        Debug.Log("Shrinking player...");
        mTransform.localScale = new Vector3(mTransform.localScale.x / growingFactor, mTransform.localScale.x / growingFactor, 1);
    }

    public void Die()
    {
        Debug.Log("Player lost.");
    }
}
