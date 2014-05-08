using System.Runtime.Remoting.Lifetime;

namespace RedGate.SSC.Windows.Remote.Services
{
    public interface ICallbacksRegistrationService : ISponsor
    {
        void Register(IScreenSelectionNotifications screenSelection);
    }
}