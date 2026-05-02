// Owner: Lee Haejun
using UnityEngine;
using UnityEngine.UI;

// Manages the settings UI for audio volume controls
public class SettingManager : MonoBehaviour
{
    public Slider bgmSlider; // Slider for background music volume
    public Slider sfxSlider; // Slider for sound effects volume

    private void Start()
    {
        // Initialize sliders with current volume values from SoundManager
        bgmSlider.value = SoundManager.Instance.GetBGMVolume();
        sfxSlider.value = SoundManager.Instance.GetSFXVolume();

        // Register listeners to update BGM volume when slider value changes
        if (bgmSlider != null && SoundManager.Instance != null)
            bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);

        // Register listeners to update SFX volume when slider value changes
        if (sfxSlider != null && SoundManager.Instance != null)
            sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
    }
}