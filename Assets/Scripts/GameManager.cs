using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LevelManager levelManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void ResetGame()
    {
        levelManager.ResetLevel();
    }
}