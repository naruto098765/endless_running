using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Animator playerAnimator;

    void Start()
    {
        // Get the Animator component attached to the player
        playerAnimator = GetComponent<Animator>();
    }

    // Called when the player collides with something
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "Obstacle" tag
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Trigger the collision animation
            PlayCollisionAnimation();
        }
    }

    void PlayCollisionAnimation()
    {
        // Check if the Animator component is available
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("CollisionTrigger", true);
            // Trigger the collision animation state using the "CollisionTrigger" parameter
            // playerAnimator.SetTrigger("CollisionTrigger");
        }

        /*else
        {
            playerAnimator.SetBool("CollisionTrigger", false);

        }*/
    }
}
