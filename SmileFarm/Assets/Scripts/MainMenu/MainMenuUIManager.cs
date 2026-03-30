using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Button BtnA;
    [SerializeField] private Button BtnB;
    [SerializeField] private Button BtnC;
    [SerializeField] private Button BtnD;

    private bool isFlowerOpen = false;  //
    private bool isShopOpen = false;    //

    private bool isUIOpen = false;      //UI 열림 여부

    void Start()
    {
        BtnA.onClick.AddListener(OnFlowerClicked);
        BtnA.onClick.AddListener(OnShopClicked);
        BtnA.onClick.AddListener(OnClicked);
        BtnA.onClick.AddListener(OnGamePlayCliked);
    }

    private void OnFlowerClicked()
    {
        if (isFlowerOpen = !isFlowerOpen)
        {

        }
    }

    private void OnShopClicked()
    {
        if (isShopOpen = !isShopOpen)
        {
            
        }
    }

    private void OnClicked()
    {
        //
    }

    private void OnGamePlayCliked()
    {
        LoadScene(2);
    }

    public void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
