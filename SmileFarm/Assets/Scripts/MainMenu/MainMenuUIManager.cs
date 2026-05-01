using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Button ProfileBtn;
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Button ARBtn;
    [SerializeField] private Button SettingBtn;
    [SerializeField] private Button GiveExpBtn;

    // 게임 테스트용
    [SerializeField] private PlayerManager playerManager;

    // Profile UI
    [SerializeField] private GameObject profilePanel;

    // Settings UI
    [SerializeField] private GameObject settingsPanel;

    private bool isProfileOpen = false;  //
    private bool isShopOpen = false;    //

    //private bool isUIOpen = false;      //UI 열림 여부

    void Start()
    {
        settingsPanel.SetActive(false);
        profilePanel.SetActive(false);

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
        profilePanel.SetActive(!profilePanel.activeSelf);
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
        settingsPanel.SetActive(!settingsPanel.activeSelf);
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

    
}