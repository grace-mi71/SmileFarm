using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Button ProfileBtn;
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Button ARBtn;
    [SerializeField] private Button SettingBtn;

    // 게임 테스트용
    [SerializeField] private PlayerManager playerManager;

    // Settings UI
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button closeSettingsBtn;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private bool isProfileOpen = false;  //
    private bool isShopOpen = false;    //

    //private bool isUIOpen = false;      //UI 열림 여부

    void Start()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);

        ProfileBtn.onClick.AddListener(OnProfileBtnClicked);
        ShopBtn.onClick.AddListener(OnShopBtnClicked);
        ARBtn.onClick.AddListener(OnARBtnClicked);
        SettingBtn.onClick.AddListener(OnSettingBtnClicked);

        // Settings UI Event Listeners
        if (closeSettingsBtn != null)
            closeSettingsBtn.onClick.AddListener(OnCloseSettingsBtnClicked);

        if (bgmSlider != null && SoundManager.Instance != null)
            bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);

        if (sfxSlider != null && SoundManager.Instance != null)
            sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
    }

    private void PlayClickSound()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayButtonClick();
    }

    private void OnProfileBtnClicked()
    {
        PlayClickSound();
        if (isProfileOpen = !isProfileOpen)
        {

        }
    }

    private void OnShopBtnClicked()
    {
        PlayClickSound();
        if (isShopOpen = !isShopOpen)
        {

        }
    }

    private void OnARBtnClicked()
    {
        PlayClickSound();
        LoadScene(2);
    }

    private void OnSettingBtnClicked()
    {
        PlayClickSound();

        // 테스트용 경험치 지급 (원하는 값으로 조정)
        if (playerManager != null)
            playerManager.ExpReward(20);

        if (settingsPanel != null)
        {
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);
        }
    }

    private void OnCloseSettingsBtnClicked()
    {
        PlayClickSound();
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
}