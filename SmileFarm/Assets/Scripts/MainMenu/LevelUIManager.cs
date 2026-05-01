using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Slider expSlider;
    public Image levelImage;

    [Header("Level Images (인덱스 0 = 레벨 1)")]
    public Sprite[] levelSprites; // Inspector에서 레벨 순서대로 할당

    private void Start()
    {
        // 이벤트 구독 - 레벨업 시 자동 호출
        PlayerManager.Instance.LevelChanged += OnLevelChanged;

        // 초기 UI 세팅
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int newLevel)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var pm = PlayerManager.Instance;

        // Slider: 현재 레벨 내 진행도
        expSlider.maxValue = pm.RequiredEXP;
        expSlider.value = pm.PlayerEXP;

        // Image: 현재 레벨에 맞는 이미지 (레벨 1 = 인덱스 0)
        int index = pm.PlayerLevel - 1;
        if (levelSprites != null && index < levelSprites.Length)
            levelImage.sprite = levelSprites[index];
    }
}