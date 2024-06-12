using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionDisplay : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public Image player1UIImage; // UI Image for Player 1
    public Image player2UIImage; // UI Image for Player 2

    void Start()
    {
        UpdatePlayerSprites();
    }

    private void UpdatePlayerSprites()
    {
        int selectedOptionPlayer1 = PlayerPrefs.GetInt("selectedOptionPlayer1", 0);
        int selectedOptionPlayer2 = PlayerPrefs.GetInt("selectedOptionPlayer2", 0);

        Character character1 = characterDB.GetCharacter(selectedOptionPlayer1);
        Character character2 = characterDB.GetCharacter(selectedOptionPlayer2);

        if (character1 != null)
        {
            player1UIImage.sprite = character1.characterSprite;
        }
        else
        {
            Debug.LogError("Character for Player 1 not found in database.");
        }

        if (character2 != null)
        {
            player2UIImage.sprite = character2.characterSprite;
        }
        else
        {
            Debug.LogError("Character for Player 2 not found in database.");
        }
    }
}
