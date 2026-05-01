using SmileFarm.Core;
using SmileFarm.Garden;
using SmileFarm.Smile;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SmileFarm.UI
{
    [DisallowMultipleComponent]
    public sealed class UIManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private SmileDetection smileDetection;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GardenGrowth gardenGrowth;

        [Header("Debug Overlay")]
        [SerializeField] private bool showDebugOverlay = true;
        [SerializeField] private Vector2 startPosition = new(16f, 16f);
        [SerializeField] private Vector2 labelSize = new(520f, 24f);
        [SerializeField] private float lineSpacing = 28f;

        [Header("Growth Gauge")]
        [SerializeField] private Slider growthSlider;

        [Header("Scene Transition")]
        [SerializeField] private bool autoLoadMainSceneOnComplete = true;
        [SerializeField] private string completeSceneName = "MainScene";
        [SerializeField] private float completeSceneDelay = 1.2f;

        [Header("Smile Session Timer")]
        [SerializeField] private bool useSmileSessionTimer = true;
        [Min(1f)]
        [SerializeField] private float sessionDurationSeconds = 20f;
        [SerializeField] private bool transferExperienceToMainMenu = true;

        // MainMenu Btn
        [Header("MainMenu Button")]
        [SerializeField] private Button FlowerButton;
        [SerializeField] private Button FlowerShop;
        [SerializeField] private Button FlowerButtonNothing;
        [SerializeField] private Button PlayGame;

        private GUIStyle labelStyle;
        private bool isLoadingCompleteScene;
        private float remainingSessionSeconds;

        private void Reset()
        {
            smileDetection = FindFirstObjectByType<SmileDetection>();
            gameManager = FindFirstObjectByType<GameManager>();
            gardenGrowth = FindFirstObjectByType<GardenGrowth>();
        }

        private void Awake()
        {
            if (smileDetection == null)
            {
                smileDetection = FindFirstObjectByType<SmileDetection>();
            }

            if (gameManager == null)
            {
                gameManager = FindFirstObjectByType<GameManager>();
            }

            if (gardenGrowth == null)
            {
                gardenGrowth = FindFirstObjectByType<GardenGrowth>();
            }

            remainingSessionSeconds = Mathf.Max(1f, sessionDurationSeconds);
            RefreshGauge();
        }

        private void OnEnable()
        {
            if (gardenGrowth != null)
            {
                gardenGrowth.ExperienceChanged += HandleExperienceChanged;
                gardenGrowth.StageChanged += HandleStageChanged;
            }
        }

        private void OnDisable()
        {
            if (gardenGrowth != null)
            {
                gardenGrowth.ExperienceChanged -= HandleExperienceChanged;
                gardenGrowth.StageChanged -= HandleStageChanged;
            }
        }

        private void Start()
        {
            RefreshGauge();
            TryLoadCompleteScene();
        }

        private void Update()
        {
            if (!useSmileSessionTimer || isLoadingCompleteScene)
            {
                return;
            }

            remainingSessionSeconds = Mathf.Max(0f, remainingSessionSeconds - Time.deltaTime);
            RefreshGauge();

            if (remainingSessionSeconds <= 0f)
            {
                if (growthSlider != null) growthSlider.value = 1f;
                BeginCompleteSceneTransition();
            }
        }

        private void OnGUI()
        {
            if (!showDebugOverlay)
            {
                return;
            }

            EnsureStyle();

            var x = startPosition.x;
            var y = startPosition.y;

            DrawLabel(x, y, "Smile Garden Debug");
            y += lineSpacing;

            DrawLabel(x, y, smileDetection != null ? smileDetection.GetDebugLabel() : "Face: SmileDetection missing");
            y += lineSpacing;

            var smileState = smileDetection != null
                ? $"Smile State: {(smileDetection.IsSmiling ? "Smiling" : "Neutral")} | Score {smileDetection.CurrentScore:0.00}"
                : "Smile State: unavailable";
            DrawLabel(x, y, smileState);
            y += lineSpacing;

            if (smileDetection != null)
            {
                DrawLabel(x, y, smileDetection.GetMetricsDebugLabel());
                y += lineSpacing;

                DrawLabel(x, y, smileDetection.GetRawMetricsDebugLabel());
                y += lineSpacing;
            }

            DrawLabel(x, y, gameManager != null ? gameManager.GetDebugLabel() : "Smile Loop: GameManager missing");
            y += lineSpacing;

            if (useSmileSessionTimer)
            {
                DrawLabel(x, y, $"Session Time: {remainingSessionSeconds:0.0}s / {sessionDurationSeconds:0.0}s");
                y += lineSpacing;
            }

            if (gardenGrowth != null)
            {
                DrawLabel(x, y, gardenGrowth.GetDebugLabel());
                y += lineSpacing;

                DrawLabel(x, y, $"Next Stage XP: {gardenGrowth.GetNextStageRequirement()}");
            }
            else
            {
                DrawLabel(x, y, "Garden: GardenGrowth missing");
            }
        }

        private void HandleExperienceChanged(int _)
        {
            RefreshGauge();
            //TryLoadCompleteScene();
        }

        private void HandleStageChanged(int _)
        {
            RefreshGauge();
            //TryLoadCompleteScene();
        }

        private void EnsureStyle()
        {
            if (labelStyle != null)
            {
                return;
            }

            labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                normal = { textColor = Color.white }
            };
        }

        private void DrawLabel(float x, float y, string text)
        {
            GUI.Label(new Rect(x, y, labelSize.x, labelSize.y), text, labelStyle);
        }

        private void RefreshGauge()
        {
            if (growthSlider == null) return;

            float elapsed = sessionDurationSeconds - remainingSessionSeconds;
            growthSlider.value = Mathf.Clamp01(elapsed / sessionDurationSeconds);
        }

        private void TryLoadCompleteScene()
        {
            if (!autoLoadMainSceneOnComplete || isLoadingCompleteScene || gardenGrowth == null)
            {
                return;
            }

            if (!gardenGrowth.IsComplete)
            {
                return;
            }

            BeginCompleteSceneTransition();
        }

        private void BeginCompleteSceneTransition()
        {
            if (isLoadingCompleteScene)
            {
                return;
            }

            isLoadingCompleteScene = true;

            if (transferExperienceToMainMenu && gardenGrowth != null)
            {
                SmileSessionTransfer.StorePendingSmileExperience(gardenGrowth.CurrentExperience);
            }

            StartCoroutine(LoadCompleteSceneAfterDelay());
        }

        private IEnumerator LoadCompleteSceneAfterDelay()
        {
            yield return new WaitForSeconds(completeSceneDelay);
            SceneManager.LoadScene(completeSceneName);
        }
    }
}
