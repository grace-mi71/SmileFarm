using UnityEngine;
using UnityEngine.UI;

public class SimpleSettingsUI : MonoBehaviour
{
    public GameObject settingsPanel; 
    public Button openBtn;           
    public Button closeBtn;          
    public Slider bgmSlider;         
    public Slider sfxSlider;        

    private void Start()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);

        if (openBtn != null) openBtn.onClick.AddListener(() => settingsPanel.SetActive(true));
        if (closeBtn != null) closeBtn.onClick.AddListener(() => settingsPanel.SetActive(false));

        if (bgmSlider != null && SoundManager.Instance != null)
        {
            bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);
        }

        if (sfxSlider != null && SoundManager.Instance != null)
        {
            sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
        }
    }
}