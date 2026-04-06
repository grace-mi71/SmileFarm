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

    private bool isProfileOpen = false;  //
    private bool isShopOpen = false;    //

    //private bool isUIOpen = false;      //UI 열림 여부

    void Start()
    {
        ProfileBtn.onClick.AddListener(OnProfileBtnClicked);
        ShopBtn.onClick.AddListener(OnShopBtnClicked);
        ARBtn.onClick.AddListener(OnARBtnClicked);
        SettingBtn.onClick.AddListener(OnSettingBtnClicked);
    }

    private void OnProfileBtnClicked()
    {
        if (isProfileOpen = !isProfileOpen)
        {

        }
    }

    private void OnShopBtnClicked()
    {
        if (isShopOpen = !isShopOpen)
        {
            
        }
    }

    private void OnARBtnClicked()
    {
        LoadScene(2);
    }

    private void OnSettingBtnClicked()
    {
         // 테스트용 경험치 지급 (원하는 값으로 조정)
        if (playerManager != null)
            playerManager.ExpReward(20);
    }

    public void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
