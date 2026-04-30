// Owner: Lee Gangmin
namespace SmileFarm.Smile
{
    // Represents a snapshot of the current smile score in a portable form.
    public readonly struct SmileSample
    {
        public SmileSample(float score)
        {
            Score = score;
        }

        public float Score { get; }

        public int Percent => UnityEngine.Mathf.RoundToInt(Score * 100f);

        public bool IsSmiling(float threshold)
        {
            // Evaluates the sample against an arbitrary threshold when needed.
            return Score >= threshold;
        }
    }
}
