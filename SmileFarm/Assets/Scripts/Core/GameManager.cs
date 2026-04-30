// Owner: Lee Gangmin
using System;
using SmileFarm.Garden;
using SmileFarm.Smile;
using UnityEngine;

namespace SmileFarm.Core
{
    [DisallowMultipleComponent]
    public sealed class GameManager : MonoBehaviour
    {
        // Connects smile detection with the AR session growth system.
        [Header("Dependencies")]
        [SerializeField] private SmileDetection smileDetection;
        [SerializeField] private GardenGrowth gardenGrowth;

        // Defines how long the player must keep smiling to earn one reward tick.
        [Header("Smile Loop")]
        [Min(0.1f)]
        [SerializeField] private float requiredSmileHoldSeconds = 2f;

        [Min(1)]
        [SerializeField] private int rewardExperience = 10;

        [Min(0f)]
        [SerializeField] private float holdResetSpeed = 2f;

        public float CurrentSmileHoldSeconds { get; private set; }

        public float HoldProgress01 => requiredSmileHoldSeconds <= 0f
            ? 1f
            : Mathf.Clamp01(CurrentSmileHoldSeconds / requiredSmileHoldSeconds);

        public bool CanAccumulateSmile => smileDetection != null && smileDetection.HasSmileInput && smileDetection.IsSmiling;

        public event Action<int> ExperienceRewarded;

        private void Reset()
        {
            smileDetection = FindFirstObjectByType<SmileDetection>();
            gardenGrowth = FindFirstObjectByType<GardenGrowth>();
        }

        private void Awake()
        {
            if (smileDetection == null)
            {
                smileDetection = FindFirstObjectByType<SmileDetection>();
            }

            if (gardenGrowth == null)
            {
                gardenGrowth = FindFirstObjectByType<GardenGrowth>();
            }
        }

        private void Update()
        {
            if (CanAccumulateSmile)
            {
                // Accumulate smile time only while valid smile input is present.
                CurrentSmileHoldSeconds += Time.deltaTime;
                TryRewardExperience();
                return;
            }

            // Smoothly reset the hold timer when the smile is lost.
            CurrentSmileHoldSeconds = Mathf.MoveTowards(CurrentSmileHoldSeconds, 0f, holdResetSpeed * Time.deltaTime);
        }

        public string GetDebugLabel()
        {
            if (smileDetection == null)
            {
                return "Smile Loop: Missing SmileDetection";
            }

            var stateLabel = CanAccumulateSmile ? "Accumulating" : "Idle";
            return $"Smile Loop: {stateLabel} | Hold {CurrentSmileHoldSeconds:0.0}s / {requiredSmileHoldSeconds:0.0}s";
        }

        private void TryRewardExperience()
        {
            if (CurrentSmileHoldSeconds < requiredSmileHoldSeconds)
            {
                return;
            }

            if (gardenGrowth != null)
            {
                // Push one reward step into the AR growth tracker.
                gardenGrowth.AddExperience(rewardExperience);
            }

            ExperienceRewarded?.Invoke(rewardExperience);
            CurrentSmileHoldSeconds = 0f;
        }
    }
}
