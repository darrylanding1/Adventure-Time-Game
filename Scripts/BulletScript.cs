using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Check if the character name is "Keith" in the character database
        if (!IsKeith())
        {
            // If the character is not "Keith", disable this script
            enabled = false;
        }
        else
        {
            Debug.Log("Attack");
            rb.velocity = transform.right * speed; // Use transform.right for 2D movement
            Destroy(gameObject, lifeTime); // Destroy the projectile after a set time
        }
    }

    bool IsKeith()
    {
        // Access your character database and check if the character name is "Keith"
        // You need to replace this with your actual method of accessing the character database
        string characterName = GetCharacterNameFromDatabase(); // Assuming you have a method to get the character name
        return characterName == "Keith";
    }

    string GetCharacterNameFromDatabase()
    {
        // Replace this with your actual method of accessing the character database
        // For demonstration purposes, returning a hardcoded name "Keith"
        return "Keith";
    }
}
