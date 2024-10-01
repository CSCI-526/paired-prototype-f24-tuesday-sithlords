using UnityEngine;

public class Well : MonoBehaviour
{
    public float speed = 2f; 
    public float xMin = -5f; 
    public float xMax = 5f;  

    public Vector2 resetPosition; 
    private Rigidbody2D ballRb;   

    void Update()
    {
        
        MoveWell();
    }

    
    void MoveWell()
    {
        
        float xPos = Mathf.PingPong(Time.time * speed, xMax - xMin) + xMin;

        
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    // Detect ball
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball")) 
        {
            ballRb = other.GetComponent<Rigidbody2D>();

            ResetBallPosition();
        }
    }

    // reset ball
    void ResetBallPosition()
    {
        
        ballRb.velocity = Vector2.zero;
        
        ballRb.position = resetPosition;        
        Debug.Log("Ball has been reset to: " + resetPosition);
    }
}
