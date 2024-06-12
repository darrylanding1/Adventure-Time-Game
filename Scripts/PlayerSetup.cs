using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public CharacterDatabase characterDB;

    void Start()
    {
        string player1AnimatorName = PlayerPrefs.GetString("player1Animator");
        string player2AnimatorName = PlayerPrefs.GetString("player2Animator");

        Animator player1Animator = player1.GetComponent<Animator>();
        Animator player2Animator = player2.GetComponent<Animator>();

        foreach (var character in characterDB.characters)
        {
            if (character.animatorController.name == player1AnimatorName)
            {
                player1Animator.runtimeAnimatorController = character.animatorController;
            }

            if (character.animatorController.name == player2AnimatorName)
            {
                player2Animator.runtimeAnimatorController = character.animatorController;
            }
        }
    }
}
