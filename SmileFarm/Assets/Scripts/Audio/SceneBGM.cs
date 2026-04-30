// Owner: Choi Eun-young
// Description: Simple trigger script to play scene-specific Background Music (BGM) via SoundManager.

using UnityEngine;

public class SceneBGM : MonoBehaviour
{
    // The specific audio clip to be played when this scene starts
    public AudioClip thisSceneBGM;

    private void Start()
    {
        // Checks if SoundManager exists and an audio clip is assigned to prevent null reference errors
        if (SoundManager.Instance != null && thisSceneBGM != null)
        {
            // Calls the PlayBGM method from the SoundManager singleton instance
            SoundManager.Instance.PlayBGM(thisSceneBGM);
        }
    }
}