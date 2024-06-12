using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthSlider;

    // Cooldown duration to prevent continuous damage
    public float damageCooldown = 1f;
    private bool canTakeDamage = true;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            TakeDamage(10);
            canTakeDamage = false; // Disable damage for the cooldown period
            Invoke("ResetDamageCooldown", damageCooldown); // Schedule cooldown reset
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthSlider.value = currentHealth;
        if (currentHealth == 0)
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        // Load the Game Over scene
        SceneManager.LoadScene("Game Over Scene");
    }

    // Reset damage cooldown
    void ResetDamageCooldown()
    {
        canTakeDamage = true;
    }
}
