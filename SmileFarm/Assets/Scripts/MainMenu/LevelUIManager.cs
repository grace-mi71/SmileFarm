// Owner: Lee Haejun
using UnityEngine;
using UnityEngine.UI;

// Manages the level-related UI elements, including the EXP slider and level image
public class LevelUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Slider expSlider;   // Slider displaying current EXP progress toward the next level
    public Image levelImage;   // Image displaying the icon corresponding to the current level

    [Header("Level Images (Index 0 = Level 1)")]
    public Sprite[] levelSprites; // Sprites assigned in order of level in the Inspector

    private void Start()
    {
        // Subscribe to level change event to automatically update UI on level up
        PlayerManager.Instance.LevelChanged += OnLevelChanged;

        // Initialize UI with the current player state on scene load
        UpdateUI();
    }

    // Unsubscribe from the level change event on destroy to prevent memory leaks
    private void OnDestroy()
    {
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.LevelChanged -= OnLevelChanged;
    }

    // Called when the player levels up - refreshes the UI with the new level data
    private void OnLevelChanged(int newLevel)
    {
        UpdateUI();
    }

    // Updates the EXP slider and level image to reflect the current player state
    private void UpdateUI()
    {
        var pm = PlayerManager.Instance;

        // Update slider to show EXP progress within the current level
        expSlider.maxValue = pm.RequiredEXP;
        expSlider.value = pm.PlayerEXP;

        // Update level image using level as index (Level 1 = Index 0)
        int index = pm.PlayerLevel - 1;
        if (levelSprites != null && index < levelSprites.Length)
            levelImage.sprite = levelSprites[index];
    }
}