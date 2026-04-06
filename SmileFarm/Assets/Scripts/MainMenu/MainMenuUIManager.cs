using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Button ProfileBtn;
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Button ARBtn;
    [SerializeField] private Button SettingBtn;

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
        
    }

    public void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
