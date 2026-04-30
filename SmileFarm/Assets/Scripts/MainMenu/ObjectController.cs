using UnityEngine;

public class LevelObjectController : MonoBehaviour
{
    [SerializeField] private GameObject[] levelObjects; // 레벨 1~10 오브젝트

    private void Start()
    {
        // 씬 로드 시 현재 레벨 상태 즉시 반영
        RefreshLevelObjects(PlayerManager.Instance.PlayerLevel);

        // 이후 레벨업 이벤트 구독
        PlayerManager.Instance.LevelChanged += OnLevelChanged;
    }

    private void OnDestroy()
    {
        // 씬 전환 시 구독 해제 (메모리 누수 방지)
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int newLevel)
    {
        RefreshLevelObjects(newLevel);
    }

    private void RefreshLevelObjects(int currentLevel)
    {
        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (levelObjects[i] != null)
                levelObjects[i].SetActive(i < currentLevel); // 레벨 이하 오브젝트 전부 활성화
        }
    }
}