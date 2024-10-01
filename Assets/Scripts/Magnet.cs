using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetStrength = 5f; 
    public float magnetRange = 5f; 
    public PolygonCollider2D boundaryPolygon; 

    private Rigidbody2D ballRb;
    private bool isBallAttached = false;
    private bool isInMagnetField = false;
    public float detachSpeedThreshold = 2f; 
    private Vector2 previousMagnetPosition; 
    private Vector2 magnetVelocity; 

    void Start()
    {
        previousMagnetPosition = transform.position;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // Check if the mouse is within the polygon boundary
        if (boundaryPolygon.OverlapPoint(mousePos))
        {
            transform.position = mousePos;
        }

        
        magnetVelocity = ((Vector2)transform.position - previousMagnetPosition) / Time.deltaTime;

        
        if (isInMagnetField && !isBallAttached)
        {
            AttractBall();
        }

        
        if (isBallAttached)
        {
            FollowMagnetWithBall();

            
            if (magnetVelocity.magnitude > detachSpeedThreshold)
            {
                DetachBall();
            }
        }

        previousMagnetPosition = transform.position; 
    }

    void AttractBall()
    {
        
        Vector2 direction = (Vector2)transform.position - ballRb.position;
        float distance = direction.magnitude;

        if (distance <= magnetRange)
        {
            Debug.Log("Ball within magnet range!");
            ballRb.AddForce(direction.normalized * magnetStrength * Time.deltaTime);

            
            if (distance <= 0.5f)
            {
                AttachBall();
            }
        }
    }

    void FollowMagnetWithBall()
    {
        
        ballRb.position = transform.position;
    }

    void DetachBall()
    {
        Debug.Log("Ball detached from magnet!");

        isBallAttached = false;

        
        ballRb.isKinematic = false;

        
        ballRb.velocity = magnetVelocity;

        Debug.Log("Ball given velocity: " + magnetVelocity);
    }

    void AttachBall()
    {
        Debug.Log("Ball attached to magnet!");

        isBallAttached = true;

        
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
