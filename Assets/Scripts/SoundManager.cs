using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S;
    private AudioSource audioSource;  // Use AudioSource instead of AudioClip
    public AudioClip gameSound;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = gameSound; // Assign the gameSound AudioClip to the AudioSource
    }

    public void PlayGameSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip); // Pass the AudioClip to PlayOneShot
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is null. Cannot play audio.");
        }
    }

    public void StopSound(){
        audioSource.Stop();
    }

}
