using System;
using UnityEngine;

namespace SmileFarm.Garden
{
    [DisallowMultipleComponent]
    public sealed class GardenGrowth : MonoBehaviour
    {
        [Header("Progression")]
        [SerializeField] private int[] stageThresholds = { 0, 20, 50, 90 };

        public int CurrentExperience { get; private set; }

        public int CurrentStageIndex { get; private set; }

        public int MaxStageIndex => Mathf.Max(0, stageThresholds.Length - 1);

        public event Action<int> ExperienceChanged;

        public event Action<int> StageChanged;

        private void OnValidate()
        {
            if (stageThresholds == null || stageThresholds.Length == 0)
            {
                stageThresholds = new[] { 0 };
            }

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

