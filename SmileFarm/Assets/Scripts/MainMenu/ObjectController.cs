// Owner: Lee Haejun
using UnityEngine;

// Controls the activation of level-based objects based on the player's current level
public class LevelObjectController : MonoBehaviour
{
    [SerializeField] private GameObject[] levelObjects; // Objects representing each level stage (Level 1~10)

    private void Start()
    {
        // Immediately reflect the current level state on scene load
        RefreshLevelObjects(PlayerManager.Instance.PlayerLevel);

        // Subscribe to level up event to handle future level changes
        PlayerManager.Instance.LevelChanged += OnLevelChanged;
    }

    // Unsubscribe from the level change event on destroy to prevent memory leaks
    private void OnDestroy()
    {
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.LevelChanged -= OnLevelChanged;
    }

    // Called when the player levels up - refreshes object states with the new level
    private void OnLevelChanged(int newLevel)
    {
        RefreshLevelObjects(newLevel);
    }

    // Activates all level objects up to and including the current level, deactivates the rest
    private void RefreshLevelObjects(int currentLevel)
    {
        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (levelObjects[i] != null)
                levelObjects[i].SetActive(i < currentLevel); // Enable objects below current level index
        }
    }
}