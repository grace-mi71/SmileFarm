using SmileFarm.Core;
using SmileFarm.Garden;
using SmileFarm.Smile;
using UnityEngine;

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

        private GUIStyle labelStyle;

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
    }
}
