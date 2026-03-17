using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace SmileFarm.Smile
{
    [DisallowMultipleComponent]
    public sealed class SmileDetection : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ARFaceManager faceManager;

        [Header("Scoring")]
        [Range(0f, 1f)]
        [SerializeField] private float smileThreshold = 0.6f;

        [Min(0f)]
        [SerializeField] private float smoothingSpeed = 8f;

        [Header("Editor Debug")]
        [SerializeField] private bool useDebugScoreInEditor = true;

        [Range(0f, 1f)]
        [SerializeField] private float debugScore = 0.25f;

        public float CurrentScore { get; private set; }

        public int CurrentPercent => Mathf.RoundToInt(CurrentScore * 100f);

        public bool IsSmiling => CurrentScore >= smileThreshold;

        public bool HasTrackedFace => FindTrackedFace();

        public event Action<float> ScoreChanged;

        public event Action<bool> SmileStateChanged;

        private bool lastSmileState;

        private void Reset()
        {
            faceManager = FindFirstObjectByType<ARFaceManager>();
        }

        private void Awake()
        {
            if (faceManager == null)
            {
                faceManager = FindFirstObjectByType<ARFaceManager>();
            }

            lastSmileState = IsSmiling;
        }

        private void Update()
        {
            var targetScore = 0f;

            if (useDebugScoreInEditor && Application.isEditor)
            {
                targetScore = debugScore;
            }
            else if (HasTrackedFace)
            {
                // Real landmark-to-score mapping will be connected here once
                // we decide which AR face metrics are stable enough on device.
                targetScore = 0f;
            }

            UpdateScore(targetScore);
        }

        public void SetSmileScore(float score)
        {
            UpdateScore(Mathf.Clamp01(score));
        }

        public void SetRawMetrics(float mouthWidthRatio, float mouthOpenRatio, float cornerLiftRatio)
        {
            var score = SmileScorer.CalculateScore(mouthWidthRatio, mouthOpenRatio, cornerLiftRatio);
            UpdateScore(score);
        }

        public SmileSample GetCurrentSample()
        {
            return new SmileSample(CurrentScore);
        }

        public string GetDebugLabel()
        {
            var trackingLabel = HasTrackedFace ? "Tracked" : "Missing";
            return $"Face: {trackingLabel} | Smile: {CurrentPercent}%";
        }

        private bool FindTrackedFace()
        {
            if (faceManager == null)
            {
                return false;
            }

            foreach (var _ in faceManager.trackables)
            {
                return true;
            }

            return false;
        }

        private void UpdateScore(float targetScore)
        {
            var previousScore = CurrentScore;
            CurrentScore = Mathf.MoveTowards(CurrentScore, targetScore, smoothingSpeed * Time.deltaTime);

            if (!Mathf.Approximately(previousScore, CurrentScore))
            {
                ScoreChanged?.Invoke(CurrentScore);
            }

            var isSmiling = IsSmiling;
            if (isSmiling != lastSmileState)
            {
                lastSmileState = isSmiling;
                SmileStateChanged?.Invoke(isSmiling);
            }
        }
    }
}
