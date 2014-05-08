namespace RedGate.SSC.Windows.Analytics
{
    public interface IAnalytics
    {
        void Track(string name, object details);
    }
}