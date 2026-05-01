using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public static MainMenuUIManager Instance;

    [SerializeField] private Button ProfileBtn;
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Button ARBtn;
    [SerializeField] private Button SettingBtn;
    [SerializeField] private Button GiveExpBtn;

    // 게임 테스트용
    [SerializeField] private PlayerManager playerManager;

    // Profile UI
    [SerializeField] private GameObject profilePanel;

    // Shop UI
    [SerializeField] private GameObject shopPanel;

    // Settings UI
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private Button[] buttons;
    private Button selectedButton;

    // Can't open UI when other UI is opening;
    private GameObject currentOpenPanel = null;

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
        settingsPanel.SetActive(false);
        profilePanel.SetActive(false);
        shopPanel.SetActive(false);

        ProfileBtn.onClick.AddListener(OnProfileBtnClicked);
        ShopBtn.onClick.AddListener(OnShopBtnClicked);
        ARBtn.onClick.AddListener(OnARBtnClicked);
        SettingBtn.onClick.AddListener(OnSettingBtnClicked);
        GiveExpBtn.onClick.AddListener(OnGiveExpBtnClicked);
    }

    private void PlayClickSound()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayButtonClick();
    }

    private void OnProfileBtnClicked()
    {
        PlayClickSound();
        OpenPanel(profilePanel);
    }

    public void OnShopBtnClicked()
    {
        PlayClickSound();
        OpenPanel(shopPanel);
    }

    private void OnARBtnClicked()
    {
        PlayClickSound();
        LoadScene(2);
    }

    private void OnSettingBtnClicked()
    {
        PlayClickSound();
        OpenPanel(settingsPanel);
    }

    private void OnGiveExpBtnClicked()
    {
        PlayClickSound();

        // 테스트용 경험치 지급 (원하는 값으로 조정)
        if (playerManager != null)
            playerManager.ExpReward(20);
    }

    public void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    private void OpenPanel(GameObject panel)
    {
        // 같은 패널 누르면 닫기
        if (currentOpenPanel == panel)
        {
            panel.SetActive(false);
            currentOpenPanel = null;
            return;
        }

        // 다른 패널이 열려있으면 먼저 닫기
        if (currentOpenPanel != null)
            currentOpenPanel.SetActive(false);

        // 새 패널 열기
        panel.SetActive(true);
        currentOpenPanel = panel;
    }
}