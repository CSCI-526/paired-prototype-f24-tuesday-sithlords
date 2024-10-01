using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalScorer : MonoBehaviour
{
    public static int score = 0;
    public GameObject Pen;
    public Vector2 penResetPosition = new Vector2(-23, -10f);
    public float moveDistance = 2f;
    public float minX = -4.5f;
    public float maxX = 4.5f;
    public static int totalObstacles = 0;
    private Collider2D goalCollider;
    private SpriteRenderer goalRenderer;
    public TextMeshProUGUI End;

    private void Start()
    {
        goalCollider = GetComponent<Collider2D>();
        goalRenderer = GetComponent<SpriteRenderer>();
        End.gameObject.SetActive(false);
        DisableGoalCollider();
    }

    private void FixedUpdate()
    {
        if (VanishingObject.obstaclesHit >= totalObstacles && !goalCollider.enabled)
        {
            EnableGoalCollider();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && goalCollider.enabled)
        {

            score++;
            Debug.Log("Score: " + score);
            ResetBall(collision.gameObject);
            End.text = "YOU WIN!";
            End.gameObject.SetActive(true);
            //MoveGoal();
            GameManager.instance.ResetGame();
        }
    }

    private void ResetBall(GameObject ball)
    {
        Rigidbody2D pen = Pen.GetComponent<Rigidbody2D>();

        ball.transform.position = new Vector2(-23, -8);

        if (pen != null)
        {
            pen.transform.position = penResetPosition;
            pen.velocity = Vector2.zero;
            pen.angularVelocity = 0;
            pen.transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }

    private void MoveGoal()
    {
        float randomDirection = Random.Range(-1f, 1f);
        float newX = transform.position.x + (randomDirection * moveDistance);
        newX = Mathf.Clamp(newX, minX, maxX);
        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }

    public Color inactiveColor = Color.red;
    public Color activeColor = Color.green;

    private void EnableGoalCollider()
    {
        goalCollider.enabled = true;
        goalRenderer.color = activeColor;
        Debug.Log("Goal enabled and turned green");
    }

    private void DisableGoalCollider()
    {
        goalCollider.enabled = false;
        goalRenderer.color = inactiveColor;
        Debug.Log("Goal disabled and turned red");
    }

    public void ResetGoal()
    {
        DisableGoalCollider();
    }
}