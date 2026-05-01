using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites; // 프레임 이미지들
    [SerializeField] private float frameRate = 0.1f; // 프레임 간격

    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneLoader.nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if (op.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % sprites.Length;
            image.sprite = sprites[currentFrame];
        }
    }
}