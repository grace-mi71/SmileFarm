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

    [Space(10)]
    [SerializeField] private float soundDelay = 0.5f; // Delay between the level-up sound and the special growth sound

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

        // 1. Always play the standard level-up sound effect
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

        // If a specific milestone is met, play the secondary sound after a delay
        if (soundToPlay != null)
        {
            StartCoroutine(PlaySpecialSoundSequence(soundToPlay));
        }
    }

    private IEnumerator PlaySpecialSoundSequence(AudioClip specialSound)
    {
        // Wait for the specified delay to prevent sounds from overlapping too harshly
        yield return new WaitForSeconds(soundDelay);
        SoundManager.Instance.PlaySFX(specialSound);
    }
}