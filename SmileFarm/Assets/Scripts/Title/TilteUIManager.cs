using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private Button gameStartBtn;
    [SerializeField] private Button exitBtn;

    void Start()
    {
        gameStartBtn.onClick.AddListener(OnMainMenuClicked);
        exitBtn.onClick.AddListener(OnExitClicked);
    }

    private void OnMainMenuClicked()
    {
        if (SoundManager.Instance != null) SoundManager.Instance.PlayButtonClick();

        SceneLoader.nextScene = "MainMenu";
        SceneManager.LoadScene("LoadingScene");
    }

    private void OnExitClicked()
    {
        if (SoundManager.Instance != null) SoundManager.Instance.PlayButtonClick();

        Application.Quit();
    }
}