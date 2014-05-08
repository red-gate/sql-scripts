using System;

namespace RedGate.SSC.Windows.Client.Credentials
{
    internal interface ICredentialsManager
    {
        void Store(string username, string password);

        bool TryReadCred(out SscLogin login);

        event EventHandler<EventArgs> Changed;

        bool Delete();
    }
}