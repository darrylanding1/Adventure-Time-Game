using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for EventTrigger


public class PauseMenu : MonoBehaviour
{
    public Canvas settingsCanvas;
    public Canvas otherCanvas; // The canvas you want to activate
    public Image imageButton;
    bool isSettingsCanvasActive = false;
    bool isOtherCanvasActive = false;
    public AudioClip backgroundMusic;

    void Start()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null && backgroundMusic != null)
        {
            audioManager.PlayBackgroundMusic(backgroundMusic);
        }

        // Check if settingsCanvas and imageButton are assigned
        if (settingsCanvas == null)
        {
            Debug.LogError("Settings canvas is not assigned!");
            return;
        }
        if (imageButton == null)
        {
            Debug.LogError("Image button is not assigned!");
            return;
        }

        Debug.Log("settingsCanvas: " + settingsCanvas.name);
        Debug.Log("imageButton: " + imageButton.name);

        // Add EventTrigger component if not already added
        if (imageButton.GetComponent<EventTrigger>() == null)
        {
            imageButton.gameObject.AddComponent<EventTrigger>();
        }

        // Add a listener to the pointer down event
        EventTrigger trigger = imageButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { ToggleCanvas(); });
        trigger.triggers.Add(entry);
    }

    void ToggleCanvas()
    {
        if (!isSettingsCanvasActive)
        {
            Debug.Log("Activating the settings canvas");

            // Activate the settings canvas
            settingsCanvas.gameObject.SetActive(true);
            isSettingsCanvasActive = true;

            // Deactivate the other canvas if it's active
            if (isOtherCanvasActive)
            {
                otherCanvas.gameObject.SetActive(false);
                isOtherCanvasActive = false;
            }
        }
        else
        {
            Debug.Log("Deactivating the settings canvas");

            // Deactivate the settings canvas
            settingsCanvas.gameObject.SetActive(false);
            isSettingsCanvasActive = false;


        }
    }
}
