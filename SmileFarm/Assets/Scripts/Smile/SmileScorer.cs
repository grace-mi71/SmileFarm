using UnityEngine;

namespace SmileFarm.Smile
{
    public static class SmileScorer
    {
        // Start simple: combine a wider smile, raised mouth corners, and a small
        // penalty for an overly open mouth so we can tune this with real device data.
        public static float CalculateScore(
            float mouthWidthRatio,
            float mouthOpenRatio,
            float cornerLiftRatio)
        {
            var widthScore = Mathf.Clamp01(mouthWidthRatio);
            var openPenalty = 1f - Mathf.Clamp01(mouthOpenRatio);
            var liftScore = Mathf.Clamp01(cornerLiftRatio);

            return Mathf.Clamp01((widthScore * 0.35f) + (liftScore * 0.55f) + (openPenalty * 0.10f));
        }
    }
}

