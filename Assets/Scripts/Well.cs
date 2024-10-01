using UnityEngine;

public class Well : MonoBehaviour
{
    public float speed = 2f; // Speed of movement for the well
    public float xMin = -5f; // Minimum X position (left boundary)
    public float xMax = 5f;  // Maximum X position (right boundary)

    public Vector2 resetPosition; // The position where the ball will reset to
    private Rigidbody2D ballRb;   // Rigidbody for the ball

    void Update()
    {
        // Move the well left and right between xMin and xMax
        MoveWell();
    }

    // Handles the movement of the well along the x-axis
    void MoveWell()
    {
        // Smooth left-right movement using Mathf.PingPong
        float xPos = Mathf.PingPong(Time.time * speed, xMax - xMin) + xMin;

        // Update the well's position while keeping y and z the same
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    // Detect when the ball enters the well's area
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball")) // Make sure the collider belongs to the ball
        {
            ballRb = other.GetComponent<Rigidbody2D>();

            // Reset the ball's position to the stored reset position
            ResetBallPosition();
        }
    }

    // Resets the ball's position and stops its movement
    void ResetBallPosition()
    {
        // Stop the ball's current movement
        ballRb.velocity = Vector2.zero;

        // Set the ball's position to the predefined resetPosition
        ballRb.position = resetPosition;

        // Optionally log the reset for debugging
        Debug.Log("Ball has been reset to: " + resetPosition);
    }
}
