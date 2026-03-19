using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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

        public bool HasSmileInput => HasTrackedFace || (useDebugScoreInEditor && Application.isEditor);

        public event Action<float> ScoreChanged;

        public event Action<bool> SmileStateChanged;

        private bool lastSmileState;
        private SmileMetrics lastMetrics;

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
                targetScore = EstimateSmileScoreFromFace();
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
            var trackingLabel = HasTrackedFace
                ? "Tracked"
                : (useDebugScoreInEditor && Application.isEditor ? "Debug" : "Missing");
            return $"Face: {trackingLabel} | Smile: {CurrentPercent}%";
        }

        public string GetMetricsDebugLabel()
        {
            return
                $"Metrics: width {lastMetrics.MouthWidthRatio:0.00} | " +
                $"open {lastMetrics.MouthOpenRatio:0.00} | " +
                $"lift {lastMetrics.CornerLiftRatio:0.00}";
        }

        public string GetRawMetricsDebugLabel()
        {
            return
                $"Raw: width {lastMetrics.RawMouthWidth:0.000} | " +
                $"open {lastMetrics.RawMouthOpen:0.000} | " +
                $"lift {lastMetrics.RawCornerLift:0.000}";
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

        private float EstimateSmileScoreFromFace()
        {
            var face = GetTrackedFace();
            if (face == null)
            {
                lastMetrics = default;
                return 0f;
            }

            if (!SmileFaceMeshEstimator.TryEstimate(face, out lastMetrics))
            {
                return 0f;
            }

            return SmileScorer.CalculateScore(
                lastMetrics.MouthWidthRatio,
                lastMetrics.MouthOpenRatio,
                lastMetrics.CornerLiftRatio);
        }

        private ARFace GetTrackedFace()
        {
            if (faceManager == null)
            {
                return null;
            }

            foreach (var trackedFace in faceManager.trackables)
            {
                if (trackedFace.trackingState == TrackingState.Tracking)
                {
                    return trackedFace;
                }
            }

            return null;
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
