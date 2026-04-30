using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // Static instance for global access (Singleton Pattern)
    public static PlayerManager Instance;
    
    [Header("Player Stats")]
    public int PlayerLevel = 1;      // 현재 레벨
    public float PlayerEXP = 0f;     // 현재 누적 경험치
    public float PlayerMoney = 0f;   // 누적 재화
    public float SmileEXP;           // 웃음으로 얻은 경험치 (누적)

    [Header("Level Settings")]
    [SerializeField] private float FirstRequiredEXP = 50f;      // 레벨 1 요구 경험치
    [SerializeField] private float expScale = 1.25f;    // 레벨업마다 요구량 배율
    [SerializeField] private int maxLevel = 10;

    public event Action<int> LevelChanged;

    // 현재 레벨업에 필요한 요구 경험치
    public float RequiredEXP => Mathf.Round(FirstRequiredEXP * Mathf.Pow(expScale, PlayerLevel));

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Start() 대신 씬 로드 이벤트 구독
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var pendingSmileExp = SmileSessionTransfer.ConsumePendingSmileExperience();
        if (pendingSmileExp <= 0) return;

        ExpReward(pendingSmileExp);
    }

    public void ExpReward(int amount)
    {
        // SmileEXP 누적
        SmileEXP += amount;

        // PlayerEXP, PlayerMoney 동시 누적
        PlayerEXP += amount;
        PlayerMoney += amount;

        // 레벨업 체크
        LevelUp();
    }

    private void LevelUp()
    {
        // 최대 레벨 도달 시 중단
        while (PlayerLevel < maxLevel && PlayerEXP >= RequiredEXP)
        {
            PlayerEXP -= RequiredEXP;
            PlayerLevel++;
            LevelChanged?.Invoke(PlayerLevel);
        }
    }
}
