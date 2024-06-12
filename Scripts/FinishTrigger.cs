using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected!");

        // Check if the collision involves a player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with finish trigger!");

            // Load the Finish scene
            SceneManager.LoadScene("Finish");
        }
    }
}
