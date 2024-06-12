using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject audioManagerObject = new GameObject("AudioManager");
                    instance = audioManagerObject.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (!audioSource.isPlaying) // Check if audio is not already playing
        {
            audioSource.clip = clip;
            audioSource.loop = true; // Ensure looping is set to true
            audioSource.Play();
        }
    }
}
