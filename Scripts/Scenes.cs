using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Scenes : MonoBehaviour
{
    public string sceneName; // Name of the scene to load, you can set it in the Unity Editor if preferred

    // Function to load a scene by name
    public void LoadSceneByName()
    {
        SceneManager.LoadScene(sceneName);
    }

    void Start()
    {
        // Get the Image component attached to this GameObject
        Image image = GetComponent<Image>();

        // Add an Event Trigger component if not already present
        if (image.gameObject.GetComponent<EventTrigger>() == null)
        {
            image.gameObject.AddComponent<EventTrigger>();
        }

        // Add a pointer click event listener for loading scene
        EventTrigger trigger = image.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entryLoadScene = new EventTrigger.Entry();
        entryLoadScene.eventID = EventTriggerType.PointerClick; // Event type is PointerClick
        entryLoadScene.callback.AddListener((data) => { LoadSceneByName(); }); // Add a callback to call LoadSceneByName when clicked
        trigger.triggers.Add(entryLoadScene);
    }
}
