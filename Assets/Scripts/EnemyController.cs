using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;           // Speed of the enemy
    public float detectionDistance = 10f;  // Distance to start following the player
    public float captureDistance = 1f;     // Distance to capture the player
    public LayerMask obstacleLayer;        // Layer for obstacles

    private Transform playerTransform;
    private bool isFollowing = false;
    private Animator enemyAnimator;  // Reference to the Animator component

    // Enable or disable enemy functionality based on this flag
    public bool isEnemyActive = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimator = GetComponent<Animator>();  // Assign the Animator component
    }

    void Update()
    {
        if (!isEnemyActive)
        {
            // Stop the animation when the enemy is not active
            if (enemyAnimator != null)
            {
                enemyAnimator.SetBool("IsRunning", false);
            }
            return;
        }

        if (Vector3.Distance(transform.position, playerTransform.position) < detectionDistance)
        {
            isFollowing = true;

            // Start or resume the running animation
            if (enemyAnimator != null)
            {
                enemyAnimator.SetBool("IsRunning", true);
            }
        }

        if (isFollowing)
        {
            MoveTowardsPlayer();
            CheckForObstacles();
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }

    void CheckForObstacles()
    {
        // Cast a ray to check for obstacles in front of the enemy
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, captureDistance, obstacleLayer))
        {
            // If an obstacle is detected, stop following and capture the player
            isFollowing = false;

            // Stop the running animation when capturing
            if (enemyAnimator != null)
            {
                enemyAnimator.SetBool("IsRunning", false);
            }

            CapturePlayer();
        }
    }

    void CapturePlayer()
    {
        // Add your capture logic here
        Debug.Log("Player captured by the enemy!");
    }
}
