    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public TextMeshProUGUI player1NameText;
    public SpriteRenderer player1ArtworkSprite;

    private int selectedOptionPlayer1 = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOptionPlayer1"))
        {
            selectedOptionPlayer1 = 0;
        }
        else
        {
            selectedOptionPlayer1 = PlayerPrefs.GetInt("selectedOptionPlayer1");
        }

        UpdateCharacter(selectedOptionPlayer1);
    }

    public void NextOption()
    {
        selectedOptionPlayer1++;
        if (selectedOptionPlayer1 >= characterDB.CharacterCount)
        {
            selectedOptionPlayer1 = 0;
        }
        UpdateCharacter(selectedOptionPlayer1);
        Save();
    }

    public void BackOption()
    {
        selectedOptionPlayer1--;
        if (selectedOptionPlayer1 < 0)
        {
            selectedOptionPlayer1 = characterDB.CharacterCount - 1;
        }
        UpdateCharacter(selectedOptionPlayer1);
        Save();
    }

    private void UpdateCharacter(int option)
    {
        Character character = characterDB.GetCharacter(option);
        player1ArtworkSprite.sprite = character.characterSprite;
        player1NameText.text = character.characterName;
    }

    private void Load()
    {
        selectedOptionPlayer1 = PlayerPrefs.GetInt("selectedOptionPlayer1");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOptionPlayer1", selectedOptionPlayer1);
    }
}
