namespace RedGate.SSC.Windows.Update.Interfaces
{
    public interface ICheckForUpdatesService
    {
        void OnStartup();
        
        void OnInteraction();
    }
}