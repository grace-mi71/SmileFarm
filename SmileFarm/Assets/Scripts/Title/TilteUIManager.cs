// Owner: Lee Haejun
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Manages UI buttons on the title screen
public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private Button gameStartBtn; // Game start button
    [SerializeField] private Button exitBtn;      // Exit button

    void Start()
    {
        // Register click event listeners for each button
        gameStartBtn.onClick.AddListener(OnMainMenuClicked);
        exitBtn.onClick.AddListener(OnExitClicked);
    }

    // Called when the game start button is clicked - navigates to Main Menu via loading scene
    private void OnMainMenuClicked()
    {
        if (SoundManager.Instance != null) SoundManager.Instance.PlayButtonClick(); // Play button click sound
        SceneLoader.nextScene = "MainMenu";          // Set destination scene after loading
        SceneManager.LoadScene("LoadingScene");      // Start loading scene
    }

    // Called when the exit button is clicked - quits the application
    private void OnExitClicked()
    {
        if (SoundManager.Instance != null) SoundManager.Instance.PlayButtonClick(); // Play button click sound
        Application.Quit(); // Quit the application
    }
}