namespace RedGate.SSC.Windows.Client
{
    internal interface IDialogController
    {
        void PromptAndStoreCredentials();
        void CloseCredentialsDialog();
    }
}