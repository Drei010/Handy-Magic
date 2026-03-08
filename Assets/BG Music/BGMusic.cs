using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; // This is the audio clip you'll attach to the script

    void Start()
    {
        if (backgroundMusic != null)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.volume = 0.4f;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Background Music clip is missing! Attach an AudioClip in the Inspector.");
        }
    }
}
