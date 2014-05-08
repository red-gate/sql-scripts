namespace RedGate.SSC.Windows.Analytics
{
    public static class AnalyticsFactory
    {
        public static IAnalytics Create()
        {
            return new Analytics();
        }
    }
}