using log4net;
using RedGate.SSC.Windows.Update.Interfaces;

namespace RedGate.SSC.Windows.Host
{
    internal class DisabledCheckForUpdateService : ICheckForUpdatesService
    {
        private static readonly ILog s_Logger = ObjectFactory.Get<ILog>();

        public void OnStartup()
        {
            s_Logger.Debug("OnStartup check suppressed");
        }

        public void OnInteraction()
        {
            s_Logger.Debug("OnInteraction check suppressed");
        }
    }
}