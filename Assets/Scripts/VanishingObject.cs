using UnityEngine;

public class VanishingObject : MonoBehaviour
{
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    public static int obstaclesHit = 0;
    private bool isVanishedPermanently = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            obstaclesHit++;

            if (obstaclesHit >= GoalScorer.totalObstacles)
            {
                isVanishedPermanently = true;
                Vanish();
            }
            else
            {
                Vanish();
                Reappear();
            }
        }
    }

    private void Vanish()
    {
        gameObject.SetActive(false);
    }

    private void Reappear()
    {
        if (!isVanishedPermanently)
        {
            Vector2 newPosition = new Vector2(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY)
            );

            transform.position = newPosition;
            gameObject.SetActive(true);
        }
    }
}