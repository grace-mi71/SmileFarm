using UnityEngine;

public class SceneBGM : MonoBehaviour
{
    public AudioClip thisSceneBGM;

    private void Start()
    {
        if (SoundManager.Instance != null && thisSceneBGM != null)
        {
            SoundManager.Instance.PlayBGM(thisSceneBGM);
        }
    }
}