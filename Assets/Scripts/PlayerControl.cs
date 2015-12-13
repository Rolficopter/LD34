using Rolficopter.LD34.Assets.Scripts;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    Rigidbody2D mRigidBody;
    Transform mTransform;
    AudioSource mAudioSource;

    public Transform groundCheck;
    public LayerMask groundMask;

    [Range(0.0f, 1000.0f)]
    public float jumpFactor = 200;
    [Range(1.01f, 2.0f)]
    [Tooltip("The amount the player grows with each jump and shrinks with each power-up.")]
    public float growingFactor = 1.01f;

    public AudioClip growSound;
    public AudioClip shrinkSound;

	// Use this for initialization
	void Start () {
        this.mRigidBody = GetComponent<Rigidbody2D>();
        this.mTransform = GetComponent<Transform>();
        this.mAudioSource = GetComponent<AudioSource>();
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

        mRigidBody.velocity = new Vector2(3, mRigidBody.velocity.y);
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

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "LeadsToDeath")
        {
            Application.LoadLevel(Constants.GetLevelName(Constants.Levels.GameOver));
        }
    }

    private void Grow()
    {
        Debug.Log("Growing player...");
        mTransform.localScale = new Vector3(mTransform.localScale.x * growingFactor, mTransform.localScale.y * growingFactor, 1);
        this.mAudioSource.clip = growSound;
        this.mAudioSource.Play();
    }

    private void Shrink()
    {
        Debug.Log("Shrinking player...");
        mTransform.localScale = new Vector3(mTransform.localScale.x / growingFactor, mTransform.localScale.y / growingFactor, 1);
        this.mAudioSource.clip = shrinkSound;
        this.mAudioSource.Play();
    }

    public void Die()
    {
        Debug.Log("Player lost.");
    }
}
