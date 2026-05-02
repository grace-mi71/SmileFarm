// Owner: Lee Haejun
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

// Handles scene loading with a frame-animated loading screen
public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Image image;          // UI Image component to display animation frames
    [SerializeField] private Sprite[] sprites;     // Array of sprites used for the loading animation
    [SerializeField] private float frameRate = 0.1f; // Time interval between each animation frame

    private int currentFrame = 0; // Index of the currently displayed frame
    private float timer = 0f;     // Elapsed time since the last frame change

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Asynchronously loads the next scene while displaying the loading screen
    IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneLoader.nextScene);
        op.allowSceneActivation = false; // Prevent automatic scene activation when loading completes

        while (!op.isDone)
        {
            // Once loading reaches 90%, wait briefly then activate the scene
            if (op.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    // Advances the loading animation frame by frame based on the defined frame rate
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % sprites.Length; // Loop back to the first frame after the last
            image.sprite = sprites[currentFrame];
        }
    }
}