using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;    // Reference to the player's transform
    public float xOffset = 0f;  // Offset in the x-axis
    public float yOffset = 0f;  // Offset in the y-axis
    public float zOffset = 0f;  // Offset in the z-axis

    void Update()
    {
        if (player != null)
        {
            // Set the follower's position with offsets
            transform.position = new Vector3(player.position.x + xOffset, player.position.y + yOffset, player.position.z + zOffset);

            // Make the follower look at the player
           // transform.LookAt(player);
        }
    }
}
