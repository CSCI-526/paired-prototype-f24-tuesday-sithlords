using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int totalObstacles = 3;
    public GameObject obstaclePrefab;
    public Transform obstacleSpawnPoint;
    public GoalScorer goalScorer;

    private void Start()
    {
        SpawnObstacles();
        Application.targetFrameRate = 60;
    }

    public void ResetLevel()
    {
        totalObstacles += 2;
        SpawnObstacles();
        goalScorer.ResetGoal();
    }

    private void SpawnObstacles()
    {
        for (int i = 0; i < totalObstacles; i++)
        {
            Instantiate(obstaclePrefab, obstacleSpawnPoint.position, Quaternion.identity);
        }
    }
}