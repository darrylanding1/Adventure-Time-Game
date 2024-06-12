using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterManagerPlayer2 : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public TextMeshProUGUI player2NameText;
    public SpriteRenderer player2ArtworkSprite;

    private int selectedOptionPlayer2 = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOptionPlayer2"))
        {
            selectedOptionPlayer2 = 0;
        }
        else
        {
            selectedOptionPlayer2 = PlayerPrefs.GetInt("selectedOptionPlayer2");
        }

        UpdateCharacter(selectedOptionPlayer2);
    }

    public void NextOption()
    {
        selectedOptionPlayer2++;
        if (selectedOptionPlayer2 >= characterDB.CharacterCount)
        {
            selectedOptionPlayer2 = 0;
        }
        UpdateCharacter(selectedOptionPlayer2);
        Save();
    }

    public void BackOption()
    {
        selectedOptionPlayer2--;
        if (selectedOptionPlayer2 < 0)
        {
            selectedOptionPlayer2 = characterDB.CharacterCount - 1;
        }
        UpdateCharacter(selectedOptionPlayer2);
        Save();
    }

    private void UpdateCharacter(int option)
    {
        Character character = characterDB.GetCharacter(option);
        player2ArtworkSprite.sprite = character.characterSprite;
        player2NameText.text = character.characterName;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOptionPlayer2", selectedOptionPlayer2);
    }

    public void ConfirmSelection()
    {
        Character character = characterDB.GetCharacter(selectedOptionPlayer2);
        PlayerPrefs.SetString("player2Animator", character.animatorController.name);
        Save();
        SceneManager.LoadScene("Level1");
    }
}
