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
        [SerializeField] private Image[] gaugeSegments = new Image[4];
        [SerializeField] private Color emptyGaugeColor = new(0.15f, 0.15f, 0.15f, 0.85f);
        [SerializeField] private Color filledGaugeColor = new(0.2f, 0.85f, 0.35f, 1f);

        [Header("Scene Transition")]
        [SerializeField] private bool autoLoadMainSceneOnComplete = true;
        [SerializeField] private string completeSceneName = "MainScene";
        [SerializeField] private float completeSceneDelay = 1.2f;

        // MainMenu Btn
        [Header("MainMenu Button")]
        [SerializeField] private Button FlowerButton;
        [SerializeField] private Button FlowerShop;
        [SerializeField] private Button FlowerButtonNothing;
        [SerializeField] private Button PlayGame;

        private GUIStyle labelStyle;
        private bool isLoadingCompleteScene;

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
            TryLoadCompleteScene();
        }

        private void HandleStageChanged(int _)
        {
            RefreshGauge();
            TryLoadCompleteScene();
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
            if (gardenGrowth == null)
            {
                return;
            }

            for (var i = 0; i < gaugeSegments.Length; i++)
            {
                var segment = gaugeSegments[i];
                if (segment == null)
                {
                    continue;
                }

                segment.type = Image.Type.Filled;
                segment.fillMethod = Image.FillMethod.Horizontal;
                segment.fillOrigin = (int)Image.OriginHorizontal.Left;
                segment.fillAmount = gardenGrowth.GetSegmentFillAmount(i, gaugeSegments.Length);
                segment.color = Color.Lerp(emptyGaugeColor, filledGaugeColor, segment.fillAmount);
            }
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

            isLoadingCompleteScene = true;
            StartCoroutine(LoadCompleteSceneAfterDelay());
        }

        private IEnumerator LoadCompleteSceneAfterDelay()
        {
            yield return new WaitForSeconds(completeSceneDelay);
            SceneManager.LoadScene(completeSceneName);
        }
    }
}
