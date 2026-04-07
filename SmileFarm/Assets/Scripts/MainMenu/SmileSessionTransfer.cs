public static class SmileSessionTransfer
{
    private static int pendingSmileExperience;

    public static void StorePendingSmileExperience(int amount)
    {
        pendingSmileExperience = amount < 0 ? 0 : amount;
    }

    public static int ConsumePendingSmileExperience()
    {
        var consumed = pendingSmileExperience;
        pendingSmileExperience = 0;
        return consumed;
    }
}
