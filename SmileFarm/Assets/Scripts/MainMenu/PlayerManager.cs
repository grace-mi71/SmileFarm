// Owner: Lee Haejun
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

// Manages player stats, experience, leveling, and persists across scenes (Singleton)
public class PlayerManager : MonoBehaviour
{
    // Static instance for global access (Singleton Pattern)
    public static PlayerManager Instance;

    [Header("Player Stats")]
    public int PlayerLevel = 1;      // Current player level
    public float PlayerEXP = 0f;     // Total accumulated experience points
    public float PlayerMoney = 0f;   // Total accumulated currency
    public float SmileEXP;           // Total experience gained specifically from smiling

    [Header("Level Settings")]
    [SerializeField] private float FirstRequiredEXP = 50f;  // EXP required to level up from level 1
    [SerializeField] private float expScale = 1.25f;        // Multiplier applied to EXP requirement per level
    [SerializeField] private int maxLevel = 10;             // Maximum level the player can reach

    // Event fired when the player levels up, passing the new level as an argument
    public event Action<int> LevelChanged;

    // Calculates the EXP required to reach the next level based on current level
    public float RequiredEXP => Mathf.Round(FirstRequiredEXP * Mathf.Pow(expScale, PlayerLevel));

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object across scene loads
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load event instead of using Start()
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances to enforce Singleton
        }
    }

    // Unsubscribe from scene load event to prevent memory leaks
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called each time a new scene is loaded - applies any pending smile EXP from the previous session
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var pendingSmileExp = SmileSessionTransfer.ConsumePendingSmileExperience();
        if (pendingSmileExp <= 0) return;
        ExpReward(pendingSmileExp);
    }

    // Rewards the player with EXP and money, then checks for level up
    public void ExpReward(int amount)
    {
        SmileEXP += amount;    // Accumulate smile-specific EXP
        PlayerEXP += amount;   // Accumulate total EXP
        PlayerMoney += amount; // Accumulate currency equal to EXP gained
        LevelUp();             // Check and process level up if threshold is reached
    }

    // Handles level up logic, supporting multiple level ups in a single reward
    private void LevelUp()
    {
        while (PlayerLevel < maxLevel && PlayerEXP >= RequiredEXP)
        {
            PlayerEXP -= RequiredEXP; // Deduct required EXP for the current level
            PlayerLevel++;            // Increment player level
            LevelChanged?.Invoke(PlayerLevel); // Notify listeners of the new level
        }
    }
}