using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Handle collision logic here (e.g., damage to the enemy)
        Destroy(gameObject); // Destroy the projectile on collision
        Debug.Log("Destroy Arrow");
    }
}
