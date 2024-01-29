using UnityEngine;
using System.Collections;
using TMPro;

public class SwipeControls : MonoBehaviour
{
    public float maxMoveDistance = 5f;
    public float forwardSpeed = 5f;
    public float jumpForce = 8f;
    public float swipeDownForce = 10f;
    public float dampingFactor = 0.7f;
    public float scoreIncrementRate = 1f; // Score increment rate per second

    private Vector2 startTouchPosition;
    private bool isGrounded;
    private bool isCollidingWithObstacle = false;
    private bool isJumping = false;
    private Rigidbody rb;
    private Animator playerAnimator;
    public TextMeshProUGUI Score_run;
    private bool shouldIncrementScore = false;

    // Variable to check if swipe controls should be active
    private bool isSwipeActive = false;

    private int score = 0; // Score variable
    private Coroutine scoreCoroutine; // Coroutine reference for score incrementation


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isCollidingWithObstacle && isSwipeActive)
        {
            MoveForward();
            CheckGrounded();
            CheckSwipeInput();
        }
    }

    void CheckSwipeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 swipeDirection = touch.position - startTouchPosition;

                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        if (swipeDirection.x < 0)
                            MoveLeft();
                        else if (swipeDirection.x > 0)
                            MoveRight();
                    }
                    else
                    {
                        if (swipeDirection.y > 0)
                            Jump();
                        else if (swipeDirection.y < 0)
                            MoveDown();
                    }
                    break;
            }
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void MoveLeft()
    {
        float newPosX = Mathf.Max(transform.position.x - maxMoveDistance, -maxMoveDistance);
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
    }

    void MoveRight()
    {
        float newPosX = Mathf.Min(transform.position.x + maxMoveDistance, maxMoveDistance);
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
    }

    void Jump()
    {
        if (isGrounded && !isJumping)
        {
            playerAnimator.SetBool("IsJumping", true);
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;

            // Call the ResetJumpAnimation after a delay
            StartCoroutine(ResetJumpAnimation(0.5f)); // You can adjust the delay as needed
        }
    }

    IEnumerator ResetJumpAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset the IsJumping parameter after the delay
        playerAnimator.SetBool("IsJumping", false);
        isJumping = false;
    }


    void MoveDown()
    {
        rb.AddForce(Vector3.down * swipeDownForce, ForceMode.Impulse);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        if (isGrounded)
        {
            isJumping = false;
            Vector3 vel = rb.velocity;
            vel.x *= dampingFactor;
            vel.z *= dampingFactor;
            rb.velocity = vel;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayObstacleCollisionAnimation();
            PauseScoreIncrement(); // Pause the score incrementation when colliding with an obstacle
        }
    }

    void PlayObstacleCollisionAnimation()
    {
        if (playerAnimator != null)
        {
            Debug.Log("Animation Played");
            playerAnimator.SetTrigger("ObstacleCollisionTrigger");

            // Stop further movement by setting speed to zero
            forwardSpeed = 0f;

            // Set the flag for obstacle collision
            isCollidingWithObstacle = true;
        }
    }

    // Function to set swipe controls active
    public void ActivateSwipeControls()
    {
        isSwipeActive = true;
    }

    // Coroutine to increment the score continuously
    void StartScoreIncrement()
    {
        if (shouldIncrementScore)
        {
            scoreCoroutine = StartCoroutine(IncreaseScore());
        }
    }
    public void SetShouldIncrementScore(bool shouldIncrement)
    {
        shouldIncrementScore = shouldIncrement;
        if (shouldIncrementScore)
        {
            StartScoreIncrement(); // Start the score incrementation coroutine if the flag is true
        }
    }

    IEnumerator IncreaseScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / scoreIncrementRate); // Wait for the specified time
            score++; // Increment the score
            UpdateScoreText(); // Update the score text
        }
    }

    // Function to pause the score incrementation
    void PauseScoreIncrement()
    {
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine); // Stop the coroutine to pause score incrementation
        }
    }

    // Function to update the score text
    void UpdateScoreText()
    {
        if (Score_run != null)
        {
            // Check if the score exceeds 999999, pause the score right there
            if (score > 999999)
            {
                score = 999999;
                PauseScoreIncrement(); // Pause score incrementation when reaching maximum score
            }

            // Update the TextMeshProUGUI object with the current score
            Score_run.text = score.ToString("D6"); // "D6" formats the score as a 6-digit string with leading zeros if necessary
        }
    }
}
