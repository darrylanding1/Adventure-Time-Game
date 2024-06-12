using UnityEngine;
using System.Collections;

public class Player2Attack : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign the projectile prefab in the Inspector
    public Transform projectileSpawnPoint; // Assign a spawn point for the projectile
    public float projectileSpeed = 10f; // Speed of the projectile
    public Animator animator; // Reference to the Animator component
    public float attackCooldown = 1f; // Cooldown time between attacks

    private bool isFacingRight = true; // Track whether the player is facing right
    private KeyCode attackKey = KeyCode.RightControl; // Attack key for Player 2
    private bool canAttack = true; // Track if the player can attack

    void Update()
    {
        // Example input for changing direction
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isFacingRight = false;
            UpdateSpawnPoint(); // Update the spawn point position
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isFacingRight = true;
            UpdateSpawnPoint(); // Update the spawn point position
        }

        if (Input.GetKeyDown(attackKey) && canAttack)
        {
            StartCoroutine(PerformAttack());
        }
    }

    void UpdateSpawnPoint()
    {
        // Adjust the projectile spawn point to the new facing direction
        Vector3 spawnPointPosition = projectileSpawnPoint.localPosition;
        spawnPointPosition.x = Mathf.Abs(spawnPointPosition.x) * (isFacingRight ? 1 : -1);
        projectileSpawnPoint.localPosition = spawnPointPosition;
    }

    IEnumerator PerformAttack()
    {
        // Play the attack animation
        animator.SetTrigger("AttackTrigger");

        // Instantiate the projectile at the spawn point
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        // Calculate the direction the player is facing
        Vector2 facingDirection = isFacingRight ? Vector2.right : Vector2.left;

        // Set the projectile's velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = facingDirection * projectileSpeed;
            Debug.Log("Projectile velocity set to: " + rb.velocity);
        }
        else
        {
            Debug.LogError("Projectile prefab is missing Rigidbody2D component.");
        }

        // Destroy the projectile after 2 seconds
        Destroy(projectile, 2f);

        // Set canAttack to false and start cooldown
        canAttack = false;

        // Wait for the cooldown period
        yield return new WaitForSeconds(attackCooldown);

        // Allow the next attack
        canAttack = true;
    }
}
