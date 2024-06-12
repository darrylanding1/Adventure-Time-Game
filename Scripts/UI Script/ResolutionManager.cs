using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionManager : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        PopulateResolutionDropdown();
        LoadResolution();
    }

    void PopulateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            string option = resolution.width + " x " + resolution.height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
    }

    void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < 0 || resolutionIndex >= resolutions.Length) return;

        Resolution selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();

        // Adjust orthographic camera size
        AdjustCameraSize(selectedResolution.width, selectedResolution.height);
    }

    void LoadResolution()
    {
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
            resolutionDropdown.value = savedResolutionIndex;
            SetResolution(savedResolutionIndex);
        }
        else
        {
            // If no resolution is saved, default to the current resolution
            int currentResolutionIndex = System.Array.FindIndex(resolutions, r => r.width == Screen.currentResolution.width && r.height == Screen.currentResolution.height);
            resolutionDropdown.value = currentResolutionIndex;
            SetResolution(currentResolutionIndex);
        }
    }

    void AdjustCameraSize(int width, int height)
    {
        float aspectRatio = (float)width / height;
        mainCamera.orthographicSize = 5f / aspectRatio; // Adjust 5f based on your design
    }
}
