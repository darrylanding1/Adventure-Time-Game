using UnityEngine;
using UnityEngine.EventSystems;

public class QuitGame : MonoBehaviour, IPointerClickHandler
{
    // Function to quit the game
    public void QuitOnClick()
    {
        Debug.Log("Quitting game..."); // Log message for debugging purposes
        Application.Quit(); // Quit the application
    }

    // Implement the IPointerClickHandler interface
    public void OnPointerClick(PointerEventData eventData)
    {
        QuitOnClick(); // Call QuitOnClick() when the Image is clicked
    }
}


