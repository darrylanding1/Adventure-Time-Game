using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Script : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public CharacterDatabase characterDatabase;
    public string characterName;

    // Variable to store the last known position of the player
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position; // Initialize lastPosition to the player's initial position
        CheckCharacterName();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Attack();
        }

        // Update lastPosition if the player has moved
        if (transform.position != lastPosition)
        {
            lastPosition = transform.position;
            UpdateFirePointDirection();
        }
    }

    void CheckCharacterName()
    {
        // Find the character in the database
        Character character = FindCharacter();
        if (character != null && character.characterName == "Keith")
        {
            enabled = true; // Enable the script if the character name is Keith
        }
        else
        {
            enabled = false; // Disable the script if the character name is not Keith
        }
    }

    Character FindCharacter()
    {
        for (int i = 0; i < characterDatabase.CharacterCount; i++)
        {
            if (characterDatabase.GetCharacter(i).characterName == characterName)
            {
                return characterDatabase.GetCharacter(i);
            }
        }
        return null;
    }

    void Attack()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void UpdateFirePointDirection()
    {
        // Calculate the direction the player is facing
        Vector3 direction = transform.localScale.x > 0 ? Vector3.right : Vector3.left;

        // Set the firePoint's position based on the player's direction
        firePoint.position = transform.position + direction * Mathf.Abs(firePoint.localPosition.x);
    }
}
