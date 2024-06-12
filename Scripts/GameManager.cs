using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int playersDead = 0;
    public int totalPlayers = 2; // Assuming there are 2 players

    private void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDied()
    {
        playersDead++;
        if (playersDead >= totalPlayers)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("Game Over Scene");
    }
}
