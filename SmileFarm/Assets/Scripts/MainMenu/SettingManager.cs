using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {   
        // Call Volume Value
        bgmSlider.value = SoundManager.Instance.GetBGMVolume();
        sfxSlider.value = SoundManager.Instance.GetSFXVolume();

        // Settings UI Event Listeners
        if (bgmSlider != null && SoundManager.Instance != null)
            bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);

        if (sfxSlider != null && SoundManager.Instance != null)
            sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
    }
}
