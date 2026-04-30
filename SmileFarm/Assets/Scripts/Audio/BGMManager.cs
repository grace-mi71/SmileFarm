// Owner: Choi Eun-young
// Description: A basic BGM manager that handles volume control via a UI Slider and toggles a settings panel using the Escape key.

using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    // The source playing the background music
    public AudioSource bgmSource;

    // UI Panel for settings that appears/disappears
    public GameObject settingsPanel;

    // UI Slider used to control the audio volume
    public Slider volumeSlider;

    void Start()
    {
        // Initialization for audio and volume slider
        if (bgmSource != null && volumeSlider != null)
        {
            // Sync the slider position with the current audio source volume
            volumeSlider.value = bgmSource.volume;

            // Dynamically link the slider's value change event to the SetVolume method
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // Ensure the settings panel is hidden when the game starts
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Toggles the settings panel visibility when the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC Key Pressed");
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);
        }
    }

    public void SetVolume(float volume)
    {
        // Directly updates the AudioSource volume (range 0.0 to 1.0)
        bgmSource.volume = volume;
    }
}