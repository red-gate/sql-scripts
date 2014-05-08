using RedGate.SSC.Windows.Client.Chromium;
using RedGate.SSC.Windows.Client.Credentials;
namespace RedGate.SSC.Windows.Client
{
    internal class CredentialsFormViewModel : IBindableToJs
    {
        private readonly ICredentialsManager m_CredentialsManager;

        public CredentialsFormViewModel()
            : this(ObjectFactory.Get<ICredentialsManager>())
        {
        }

        private CredentialsFormViewModel(ICredentialsManager credentialsManager)
        {
            m_CredentialsManager = credentialsManager;
        }

        public void SaveCredentialsToStore(string username, string password)
        {
            m_CredentialsManager.Store(username, password);
            ObjectFactory.Get<IDialogController>().CloseCredentialsDialog();
        }

        public string Name
        {
            get { return "RedGate_SSC_Windows_Client_CredentialsFormViewModel"; }
        }
    }
}