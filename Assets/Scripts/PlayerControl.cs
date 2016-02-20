using Rolficopter.LD34.Assets.Scripts;
using UnityEngine;

namespace Rolficopter.LD34.Assets.Scripts
{
    public class PlayerControl : MonoBehaviour
    {

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
        [Range(1.0f, 100.0f)]
        [Tooltip("The constant velocity of the player to the right.")]
        public float velocity = 5.0f;

        public AudioClip growSound;
        public AudioClip shrinkSound;

        [Range(1.0f, 100.0f)]
        public int growSpeed = 50;
        private Vector3 mTargetScale;

        private Vector3 startScale = Vector3.one;
        private bool isPressingJump;
        private bool isPressingDown;
        private bool wasPressingDown;
        private bool wasPressingUp;

        // Use this for initialization
        void Start()
        {
#if UNITY_EDITOR
            Input.simulateMouseWithTouches = true;
#endif
            this.mRigidBody = GetComponent<Rigidbody2D>();
            this.mTransform = GetComponent<Transform>();
            this.mAudioSource = GetComponent<AudioSource>();

            this.mTargetScale = mTransform.localScale;
            this.startScale = this.mTargetScale;
        }

        // Update is called once per frame
        void Update()
        {
            isPressingJump = (Input.GetButtonDown("Jump") || Input.GetAxis("Shrink") < 0) || isPressingJump;
            isPressingDown = Input.GetAxis("Shrink") > 0 || isPressingDown;
           

            if (isPressingJump && !wasPressingUp && groundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(groundMask))
            {
                Jump();
                mRigidBody.AddForce(Vector2.up * jumpFactor);
                this.Grow();
            }

            if (isPressingDown && !wasPressingDown)
            {
                // check for down
                GameObject powerUp = this.GetCollidingPowerUp();
                if (powerUp != null)
                {
                    PowerUpLogic powerUpLogic = powerUp.GetComponent<PowerUpLogic>();

                    if (!powerUpLogic.isExhausted)
                    {
                        powerUpLogic.Exhaust();

                        this.Shrink(); 

                    }
                }
            }

            mRigidBody.velocity = new Vector2(this.velocity, mRigidBody.velocity.y);

            mTransform.localScale = Vector3.Lerp(mTransform.localScale, mTargetScale, Time.deltaTime * this.growSpeed);

            wasPressingUp = isPressingJump;
            wasPressingDown = isPressingDown;
            isPressingJump = false;
            isPressingDown = false;
        }

        public void Jump()
        {
            isPressingJump = true;
        }

        public void DeJump()
        {
            isPressingDown = true;
        }

        private GameObject GetCollidingPowerUp()
        {
            GameObject powerUpsNode = GameObject.Find("PowerUps");
            if (powerUpsNode != null)
            {
                EdgeCollider2D playerCollider = this.gameObject.GetComponent<EdgeCollider2D>();

                foreach (var powerUpObject in powerUpsNode.transform)
                {
                    GameObject powerUp = ((Transform)powerUpObject).gameObject;
                    BoxCollider2D powerUpCollider = powerUp.GetComponent<BoxCollider2D>();
                    if (playerCollider.IsTouching(powerUpCollider))
                    {
                        return powerUp;
                    }
                }
            }

            return null;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "LeadsToDeath")
            {
                Die();
            }
            else if (col.gameObject.tag == "Finish")
            {
                NextLevel();
            }
        }

        private void NextLevel()
        {
            Debug.Log("Ohjo!");
            Application.LoadLevel(Constants.GetLevelName(Constants.Levels.LevelFinished));
        }


        private void Grow()
        {
            mTargetScale = new Vector3(mTargetScale.x * growingFactor, mTargetScale.y * growingFactor, 1);
            this.mAudioSource.clip = growSound;
            this.mAudioSource.Play();
            if ( mTargetScale.y > this.startScale.y * 2 )
            {
                this.mTargetScale = this.startScale * 2;
                return;
            }

            Debug.Log("Growing player...");
            
        }

        private void Shrink()
        {
            mTargetScale = new Vector3(mTargetScale.x / growingFactor, mTargetScale.y / growingFactor, 1);

            // WAT
            /*if ( mTargetScale.y < this.startScale.y )
            {
                this.mTargetScale = this.startScale;
                return;
            }*/

            Debug.Log("Shrinking player...");
            this.mAudioSource.clip = shrinkSound;
            this.mAudioSource.Play();
        }

        public void Die()
        {
            Debug.Log("Player lost.");
            if (Application.loadedLevelName.Equals("Level1"))
            {
                this.mTransform.position = new Vector3(70, this.mTransform.position.y, this.mTransform.position.z);
                this.mTargetScale = this.startScale;
                
                GameObject[] allTexts = GameObject.FindGameObjectsWithTag("PowerUp");
                foreach (var text in allTexts)
                {
                    PowerUpLogic powerUp = text.GetComponent<PowerUpLogic>();
                    if (powerUp != null)
                    {
                        powerUp.Reset();
                    }
                }
            }
            else
            {
                Application.LoadLevel(Constants.GetLevelName(Constants.Levels.GameOver));
            }
        }
    }
}