// Owner: Lee Gangmin
using System;
using UnityEngine;

namespace SmileFarm.Garden
{
    [DisallowMultipleComponent]
    public sealed class GardenGrowth : MonoBehaviour
    {
        // Defines AR-session progression milestones for flower growth.
        [Header("Progression")]
        [SerializeField] private int[] stageThresholds = { 0, 20, 50, 90 };

        public int CurrentExperience { get; private set; }

        public int CurrentStageIndex { get; private set; }

        public int MaxStageIndex => Mathf.Max(0, stageThresholds.Length - 1);

        public bool IsComplete => CurrentExperience >= stageThresholds[MaxStageIndex];

        public event Action<int> ExperienceChanged;

        public event Action<int> StageChanged;

        private void OnValidate()
        {
            if (stageThresholds == null || stageThresholds.Length == 0)
            {
                stageThresholds = new[] { 0 };
            }

            // Keep thresholds ordered so stage evaluation stays predictable.
            stageThresholds[0] = 0;

            for (var i = 1; i < stageThresholds.Length; i++)
            {
                if (stageThresholds[i] < stageThresholds[i - 1])
                {
                    stageThresholds[i] = stageThresholds[i - 1];
                }
            }
        }

        public void AddExperience(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            // Track cumulative AR session EXP and notify listeners immediately.
            CurrentExperience += amount;
            ExperienceChanged?.Invoke(CurrentExperience);

            var nextStage = EvaluateStage(CurrentExperience);
            if (nextStage == CurrentStageIndex)
            {
                return;
            }

            CurrentStageIndex = nextStage;
            StageChanged?.Invoke(CurrentStageIndex);
        }

        public int GetNextStageRequirement()
        {
            var nextStageIndex = Mathf.Min(CurrentStageIndex + 1, MaxStageIndex);
            return stageThresholds[nextStageIndex];
        }

        public float GetSegmentFillAmount(int segmentIndex, int totalSegments)
        {
            if (segmentIndex < 0 || totalSegments <= 0 || segmentIndex >= totalSegments)
            {
                return 0f;
            }

            // Converts overall progress into per-segment UI fill values.
            var scaledProgress = GetOverallProgress01() * totalSegments;
            return Mathf.Clamp01(scaledProgress - segmentIndex);
        }

        public float GetOverallProgress01()
        {
            var finalRequirement = stageThresholds[MaxStageIndex];
            if (finalRequirement <= 0)
            {
                return 1f;
            }

            return Mathf.Clamp01(CurrentExperience / (float)finalRequirement);
        }

        public string GetDebugLabel()
        {
            return $"Garden: Stage {CurrentStageIndex + 1}/{MaxStageIndex + 1} | XP {CurrentExperience}";
        }

        private int EvaluateStage(int experience)
        {
            var stageIndex = 0;

            for (var i = 0; i < stageThresholds.Length; i++)
            {
                if (experience >= stageThresholds[i])
                {
                    stageIndex = i;
                }
            }

            return stageIndex;
        }
    }
}
