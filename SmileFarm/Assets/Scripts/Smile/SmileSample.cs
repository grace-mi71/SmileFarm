namespace SmileFarm.Smile
{
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
            return Score >= threshold;
        }
    }
}

