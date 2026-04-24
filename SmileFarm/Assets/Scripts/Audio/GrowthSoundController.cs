using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthSoundController : MonoBehaviour
{
    // <변경> 쳐다보는 대상을 PlayerManager로 바꿨습니다!
    [SerializeField] private PlayerManager playerManager;

    [Header("Level up Sound")]
    [SerializeField] private AudioClip levelUpSound;

    [Header("Animation Sound")]
    [SerializeField] private AudioClip flowerGrowSound;
    [SerializeField] private List<int> flowerStages;

    [SerializeField] private AudioClip treeGrowSound;
    [SerializeField] private List<int> treeStages;

    [SerializeField] private AudioClip furnitureSound;
    [SerializeField] private List<int> furnitureStages;

    [SerializeField] private AudioClip birdSound;
    [SerializeField] private List<int> birdStages;

    [Space(10)]
    [SerializeField] private float soundDelay = 0.5f;

    private void Start()
    {
        if (playerManager != null)
        {
            // <변경> PlayerManager의 레벨업 확성기를 구독합니다.
            playerManager.LevelChanged += OnLevelChanged;
        }
    }

    private void OnDestroy()
    {
        if (playerManager != null)
        {
            playerManager.LevelChanged -= OnLevelChanged;
        }
    }

    // 이제 currentLevel은 농장의 찐 레벨(1~10)이 들어옵니다.
    private void OnLevelChanged(int currentLevel)
    {
        if (SoundManager.Instance == null) return;

        // 1. 레벨업 소리 무조건 재생
        SoundManager.Instance.PlaySFX(levelUpSound);

        AudioClip soundToPlay = null;

        // 2. 지금 농장 레벨이 설정해둔 리스트에 있는지 검사!
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

        if (soundToPlay != null)
        {
            StartCoroutine(PlaySpecialSoundSequence(soundToPlay));
        }
    }

    private IEnumerator PlaySpecialSoundSequence(AudioClip specialSound)
    {
        yield return new WaitForSeconds(soundDelay);
        SoundManager.Instance.PlaySFX(specialSound);
    }
}