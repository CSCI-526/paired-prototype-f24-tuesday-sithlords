using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetStrength = 5f; // The strength of the magnetic pull
    public float magnetRange = 5f; // The range of the magnetic field
    public PolygonCollider2D boundaryPolygon; // The boundary where the magnet can move

    private Rigidbody2D ballRb;
    private bool isBallAttached = false;
    private bool isInMagnetField = false;
    public float detachSpeedThreshold = 2f; // Speed at which the ball detaches
    private Vector2 previousMagnetPosition; // To track the speed of the magnet
    private Vector2 magnetVelocity; // To track the magnet's velocity

    void Start()
    {
        previousMagnetPosition = transform.position;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // Move magnet with mouse inside polygon boundary
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // Check if the mouse is within the polygon boundary
        if (boundaryPolygon.OverlapPoint(mousePos))
        {
            transform.position = mousePos;
        }

        // Calculate the magnet's velocity
        magnetVelocity = ((Vector2)transform.position - previousMagnetPosition) / Time.deltaTime;

        // If the ball is within the magnetic field and not yet attached
        if (isInMagnetField && !isBallAttached)
        {
            AttractBall();
        }

        // If the ball is attached, move it with the magnet
        if (isBallAttached)
        {
            FollowMagnetWithBall();

            // Check if the magnet is moving too fast to detach the ball
            if (magnetVelocity.magnitude > detachSpeedThreshold)
            {
                DetachBall();
            }
        }

        previousMagnetPosition = transform.position; // Track the magnet's position to calculate velocity
    }

    void AttractBall()
    {
        // Attract the ball towards the magnet if it's within range
        Vector2 direction = (Vector2)transform.position - ballRb.position;
        float distance = direction.magnitude;

        if (distance <= magnetRange)
        {
            Debug.Log("Ball within magnet range!");
            ballRb.AddForce(direction.normalized * magnetStrength * Time.deltaTime);

            // Attach the ball if it gets close enough
            if (distance <= 0.5f)
            {
                AttachBall();
            }
        }
    }

    void FollowMagnetWithBall()
    {
        // Instead of making the ball kinematic, just move the ball with the magnet
        ballRb.position = transform.position;
    }

    void DetachBall()
    {
        Debug.Log("Ball detached from magnet!");

        isBallAttached = false;

        // Restore normal physics on the ball
        ballRb.isKinematic = false;

        // Apply the magnet's velocity to the ball when detaching
        ballRb.velocity = magnetVelocity;

        Debug.Log("Ball given velocity: " + magnetVelocity);
    }

    void AttachBall()
    {
        Debug.Log("Ball attached to magnet!");

        isBallAttached = true;

        // Set the ball's velocity to zero when attached to the magnet
        ballRb.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball entered magnet field");
            ballRb = other.GetComponent<Rigidbody2D>();
            isInMagnetField = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball exited magnet field");
            isInMagnetField = false;
        }
    }
}
