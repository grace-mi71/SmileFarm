using UnityEngine;

namespace SmileFarm.Garden
{
    [DisallowMultipleComponent]
    public sealed class FaceFlowerStageView : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GardenGrowth gardenGrowth;
        [SerializeField] private FaceFlowerAnchor flowerAnchor;

        [Header("Flower Slots")]
        [SerializeField] private GameObject[] flowerSlots = new GameObject[4];

        private void Reset()
        {
            gardenGrowth = FindFirstObjectByType<GardenGrowth>();
            flowerAnchor = GetComponent<FaceFlowerAnchor>();
        }

        private void Awake()
        {
            if (gardenGrowth == null)
            {
                gardenGrowth = FindFirstObjectByType<GardenGrowth>();
            }

            if (flowerAnchor == null)
            {
                flowerAnchor = GetComponent<FaceFlowerAnchor>();
            }
        }

        private void OnEnable()
        {
            if (gardenGrowth != null)
            {
                gardenGrowth.StageChanged += HandleStageChanged;
            }

            RefreshFlowers();
        }

        private void OnDisable()
        {
            if (gardenGrowth != null)
            {
                gardenGrowth.StageChanged -= HandleStageChanged;
            }
        }

        private void LateUpdate()
        {
            if (flowerAnchor == null)
            {
                return;
            }

            if (!flowerAnchor.HasTrackedFace)
            {
                SetFlowersActive(0);
            }
            else
            {
                RefreshFlowers();
            }
        }

        private void HandleStageChanged(int _)
        {
            RefreshFlowers();
        }

        private void RefreshFlowers()
        {
            var activeCount = 0;

            if (gardenGrowth != null && (flowerAnchor == null || flowerAnchor.HasTrackedFace))
            {
                activeCount = Mathf.Clamp(gardenGrowth.CurrentStageIndex + 1, 0, flowerSlots.Length);
            }

            SetFlowersActive(activeCount);
        }

        private void SetFlowersActive(int activeCount)
        {
            for (var i = 0; i < flowerSlots.Length; i++)
            {
                if (flowerSlots[i] == null)
                {
                    continue;
                }

                flowerSlots[i].SetActive(i < activeCount);
            }
        }
    }
}
