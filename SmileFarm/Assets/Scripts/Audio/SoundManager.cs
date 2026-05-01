// Owner: Choi Eun-young
// Description: Core Sound Manager using the Singleton pattern to manage BGM, SFX, and AudioMixer parameters globally.

using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Static instance for global access (Singleton Pattern)
    public static SoundManager Instance;

    // Reference to the Unity Audio Mixer for volume control
    public AudioMixer audioMixer;

    // Dedicated sources for different audio types to allow overlapping/independent control
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    // Default clip for UI interaction sounds
    public AudioClip defaultClickSound;

    // Default volume value
    private const float drfaultVolume = 0.5f;

    private void Awake()
    {
        // Implementation of the Singleton pattern to ensure only one SoundManager exists
        if (Instance == null)
        {
            Instance = this;
            // Keeps the SoundManager alive when switching between different scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroys duplicate instances that may be created upon reloading scenes
            Destroy(gameObject);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        // Prevents restarting the track if the same clip is already playing
        if (bgmSource.clip == clip) return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        // Plays a short sound effect once without interrupting currently playing SFX
        sfxSource.PlayOneShot(clip);
    }

    public void PlayButtonClick()
    {
        // Helper method to play a standard UI click sound globally
        if (defaultClickSound != null)
        {
            PlaySFX(defaultClickSound);
        }
    }

    // Save volume value and call value other script
    public float GetBGMVolume() => PlayerPrefs.GetFloat("BGMVolume", drfaultVolume);
    public float GetSFXVolume() => PlayerPrefs.GetFloat("SFXVolume", drfaultVolume);

    public void SetBGMVolume(float volume)
    {   
        // Converts linear slider value (0 to 1) to logarithmic decibels (dB) for the AudioMixer
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        // Converts linear slider value (0 to 1) to logarithmic decibels (dB) for the AudioMixer
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}