//eunyoung

using UnityEngine;
using UnityEngine.UI; 

public class BGMManager: MonoBehaviour
{
    public AudioSource bgmSource;      
    public GameObject settingsPanel;   
    public Slider volumeSlider;       

    void Start()
    {
        if (bgmSource != null && volumeSlider != null)
        {
            volumeSlider.value = bgmSource.volume;

            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    void Update()
    {
        // ESC 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC Å° ´­¸²");
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);
        }
    }

    public void SetVolume(float volume)
    {
        bgmSource.volume = volume;
    }
}