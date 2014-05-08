using System.Runtime.Remoting.Lifetime;
using RedGate.AppHost.Interfaces;

namespace RedGate.SSC.Windows.Remote.Services
{
    public interface ISsmsOperations : ISponsor
    {
        void CreateAugmentedQueryWindow(string sqlScript, string title, IRemoteElement remoteElement);
    }
}