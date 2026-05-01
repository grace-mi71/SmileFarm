// Owner: Choi Eun-young
// Description: Controller that plays specific growth and milestone sound effects by observing level changes from the PlayerManager.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthSoundController : MonoBehaviour
{
    // Reference to the PlayerManager to observe level-up events
    [SerializeField] private PlayerManager playerManager;

    [Header("Level up Sound")]
    [SerializeField] private AudioClip levelUpSound;

    [Header("Animation Sound")]
    [SerializeField] private AudioClip flowerGrowSound;
    [SerializeField] private List<int> flowerStages; // List of levels that trigger the flower growth sound

    [SerializeField] private AudioClip treeGrowSound;
    [SerializeField] private List<int> treeStages; // List of levels that trigger the tree growth sound

    [SerializeField] private AudioClip furnitureSound;
    [SerializeField] private List<int> furnitureStages; // List of levels that trigger the furniture sound

    [SerializeField] private AudioClip birdSound;
    [SerializeField] private List<int> birdStages; // List of levels that trigger the bird sound

    [Header("Sound Delays")]
    [SerializeField] private float levelUpDelay = 1.0f; // Delay before playing the standard level-up sound
    [SerializeField] private float specialSoundDelay = 1.5f; // Additional delay after the level-up sound before playing the special growth sound

    private void Start()
    {
        if (playerManager != null)
        {
            // Subscribe to the LevelChanged event from PlayerManager
            playerManager.LevelChanged += OnLevelChanged;
        }
    }

    private void OnDestroy()
    {
        if (playerManager != null)
        {
            // Unsubscribe to prevent memory leaks and null reference errors when the object is destroyed
            playerManager.LevelChanged -= OnLevelChanged;
        }
    }

    // Triggered when the farm level (1-10) changes
    private void OnLevelChanged(int currentLevel)
    {
        if (SoundManager.Instance == null) return;

        // Start the coroutine to play sounds in sequence with delays
        StartCoroutine(PlaySoundSequence(currentLevel));
    }

    private IEnumerator PlaySoundSequence(int currentLevel)
    {
        // 1. Wait for the specified delay before playing the standard level-up sound
        yield return new WaitForSeconds(levelUpDelay);
        SoundManager.Instance.PlaySFX(levelUpSound);

        AudioClip soundToPlay = null;

        // 2. Check if the current level matches any specific growth milestones
        if (flowerStages.Contains(currentLevel))
        {
            soundToPlay = flowerGrowSound;
        }
        else if (treeStages.Contains(currentLevel))
        {
            soundToPlay = treeGrowSound;
        }
        else if (furnitureStages.Contains(currentLevel))
        {
            soundToPlay = furnitureSound;
        }
        else if (birdStages.Contains(currentLevel))
        {
            soundToPlay = birdSound;
        }

        // 3. If a specific milestone is met, play the secondary sound after an additional delay
        if (soundToPlay != null)
        {
            yield return new WaitForSeconds(specialSoundDelay);
            SoundManager.Instance.PlaySFX(soundToPlay);
        }
    }
}