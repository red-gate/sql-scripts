using System;
using System.Linq;
using CredentialManagement;
using RedGate.SSC.Windows.Product;

namespace RedGate.SSC.Windows.Client.Credentials
{
    internal class WindowsCryptStoreCredentialsManager : ICredentialsManager
    {
        public event EventHandler<EventArgs> Changed;

        public void Store(string username, string password)
        {
            using (var cred = new Credential(username, password, TheProduct.CredentialsTarget, CredentialType.Generic)
                {
                    PersistanceType = PersistanceType.Enterprise
                })
            {
                if (cred.Save())
                {
                    OnChanged();
                }
                else
                {
                    throw new InvalidOperationException("Couldn't save credential to store");
                }
            }
        }

        public bool TryReadCred(out SscLogin login)
        {
            using (CredentialSet credentialSet = new CredentialSet(TheProduct.CredentialsTarget))
            {
                Credential credential = credentialSet.Load().SingleOrDefault();

                if (credential != null)
                {
                    login = new SscLogin(credential.Username, credential.Password);
                    return true;
                }

                login = null;
                return false;
            }
        }

        public bool Delete()
        {
            using (CredentialSet credentialSet = new CredentialSet(TheProduct.CredentialsTarget))
            {
                Credential cred = credentialSet.Load().FirstOrDefault();

                if (cred != null)
                    OnChanged();

                return cred != null && cred.Delete();
            }
        }

        private void OnChanged()
        {
            var handler = Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}