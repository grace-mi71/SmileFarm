// Owner: Lee Gangmin
namespace SmileFarm.Smile
{
    // Bundles normalized and raw mouth metrics for scoring and debug output.
    public readonly struct SmileMetrics
    {
        public SmileMetrics(
            float mouthWidthRatio,
            float mouthOpenRatio,
            float cornerLiftRatio,
            float rawMouthWidth,
            float rawMouthOpen,
            float rawCornerLift)
        {
            MouthWidthRatio = mouthWidthRatio;
            MouthOpenRatio = mouthOpenRatio;
            CornerLiftRatio = cornerLiftRatio;
            RawMouthWidth = rawMouthWidth;
            RawMouthOpen = rawMouthOpen;
            RawCornerLift = rawCornerLift;
        }

        public float MouthWidthRatio { get; }

        public float MouthOpenRatio { get; }

        public float CornerLiftRatio { get; }

        public float RawMouthWidth { get; }

        public float RawMouthOpen { get; }

        public float RawCornerLift { get; }
    }
}

