using System.Windows.Forms;

namespace RedGate.SSC.Windows.Client
{
    internal class DialogController : IDialogController
    {
        private readonly ApplicationDispatcher m_ApplicationDispatcher;
        private Form m_CredentialsForm;
        
        public DialogController(ApplicationDispatcher applicationDispatcher)
        {
            m_ApplicationDispatcher = applicationDispatcher;
            
        }

        public void PromptAndStoreCredentials()
        {
            m_ApplicationDispatcher.Invoke(PromptAndStoreCredentialsInner);
        }

        private void PromptAndStoreCredentialsInner()
        {
            //TODO: Can't make Showdialog() work here...
            m_CredentialsForm = new CredentialsForm();
            m_CredentialsForm.Show();
        }

        public void CloseCredentialsDialog()
        {
            m_ApplicationDispatcher.Invoke(CloseCredentialsDialogInner);
        }

        private void CloseCredentialsDialogInner()
        {
            if (m_CredentialsForm != null)
                m_CredentialsForm.Close();
        }
    }
}