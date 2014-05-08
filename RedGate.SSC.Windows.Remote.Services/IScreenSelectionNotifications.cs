using System.Runtime.Remoting.Lifetime;

namespace RedGate.SSC.Windows.Remote.Services
{
    public interface IScreenSelectionNotifications : ISponsor
    {
        void ShowShareScreen(string script);
    }
}
