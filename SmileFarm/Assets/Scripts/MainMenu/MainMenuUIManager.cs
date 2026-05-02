// Owner: Lee Haejun
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Manages the Main Menu UI, handling panel navigation and button interactions (Singleton)
public class MainMenuUIManager : MonoBehaviour
{
    public static MainMenuUIManager Instance;

    [SerializeField] private Button ProfileBtn;  // Button to open the profile panel
    [SerializeField] private Button ShopBtn;     // Button to open the shop panel
    [SerializeField] private Button ARBtn;       // Button to navigate to the AR scene
    [SerializeField] private Button SettingBtn;  // Button to open the settings panel

    [SerializeField] private PlayerManager playerManager; // Reference used for testing EXP rewards

    // UI Panels
    [SerializeField] private GameObject profilePanel;  // Profile settings panel
    [SerializeField] private GameObject shopPanel;     // Shop panel
    [SerializeField] private GameObject settingsPanel; // Settings panel

    private GameObject currentOpenPanel = null; // Tracks the currently open panel to prevent multiple panels opening simultaneously

    // Enforce Singleton pattern
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Ensure all panels are closed on startup
        settingsPanel.SetActive(false);
        profilePanel.SetActive(false);
        shopPanel.SetActive(false);

        // Register click event listeners for each button
        ProfileBtn.onClick.AddListener(OnProfileBtnClicked);
        ShopBtn.onClick.AddListener(OnShopBtnClicked);
        ARBtn.onClick.AddListener(OnARBtnClicked);
        SettingBtn.onClick.AddListener(OnSettingBtnClicked);
    }

    // Plays button click sound if SoundManager is available
    private void PlayClickSound()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayButtonClick();
    }

    // Opens the profile panel
    private void OnProfileBtnClicked()
    {
        PlayClickSound();
        OpenPanel(profilePanel);
    }

    // Opens the shop panel
    public void OnShopBtnClicked()
    {
        PlayClickSound();
        OpenPanel(shopPanel);
    }

    // Navigates to the AR scene via the loading screen
    private void OnARBtnClicked()
    {
        PlayClickSound();
        SceneLoader.nextScene = "ARScene";
        SceneManager.LoadScene("LoadingScene");
    }

    // Opens the settings panel
    private void OnSettingBtnClicked()
    {
        PlayClickSound();
        OpenPanel(settingsPanel);
    }

    // Grants test EXP to the player (for development/testing purposes only)
    private void OnGiveExpBtnClicked()
    {
        PlayClickSound();
        if (playerManager != null)
            playerManager.ExpReward(20);
    }

    // Loads a scene by build index
    public void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    // Toggles the given panel open or closed, closing any other open panel first
    private void OpenPanel(GameObject panel)
    {
        // If the same panel is clicked again, close it
        if (currentOpenPanel == panel)
        {
            panel.SetActive(false);
            currentOpenPanel = null;
            return;
        }

        // Close the currently open panel before opening a new one
        if (currentOpenPanel != null)
            currentOpenPanel.SetActive(false);

        // Open the new panel and track it as the current open panel
        panel.SetActive(true);
        currentOpenPanel = panel;
    }
}