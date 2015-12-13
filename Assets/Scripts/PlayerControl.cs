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

        private Vector2 mLastTouchPosition;

        private Vector3 mTargetScale;

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
        }

        // Update is called once per frame
        void Update()
        {
            bool isPressingJump = Input.GetButtonDown("Jump");
            bool isPressingDown = Input.GetButtonDown("Shrink");

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        mLastTouchPosition = Input.GetTouch(i).position;
                    }

                    if (Input.GetTouch(i).phase == TouchPhase.Ended || Input.GetTouch(i).phase == TouchPhase.Stationary)
                    {
                        Vector2 deltaPosition = Input.GetTouch(i).position - mLastTouchPosition;

                        if (deltaPosition.y > 0)
                        {
                            isPressingJump = true;
                        }
                        else if (deltaPosition.y < 0)
                        {
                            isPressingDown = true;
                        }
                    }
                }
            }

            if (isPressingJump && groundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(groundMask))
            {
                mRigidBody.AddForce(Vector2.up * jumpFactor);
                this.Grow();
            }

            if (isPressingDown)
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

            mTransform.localScale = Vector3.Lerp(mTransform.localScale, mTargetScale, Time.deltaTime * 10);
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
            Debug.Log("Growing player...");
            mTargetScale = new Vector3(mTargetScale.x * growingFactor, mTargetScale.y * growingFactor, 1);
            this.mAudioSource.clip = growSound;
            this.mAudioSource.Play();
        }

        private void Shrink()
        {
            Debug.Log("Shrinking player...");
            mTargetScale = new Vector3(mTargetScale.x / growingFactor, mTargetScale.y / growingFactor, 1);
            this.mAudioSource.clip = shrinkSound;
            this.mAudioSource.Play();
        }

        public void Die()
        {
            Debug.Log("Player lost.");
            Application.LoadLevel(Constants.GetLevelName(Constants.Levels.GameOver));
        }
    }
}