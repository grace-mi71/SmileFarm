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
        LoadScene(1);
    }

    private void OnExitClicked()
    {
        Application.Quit();
    }

    public void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
