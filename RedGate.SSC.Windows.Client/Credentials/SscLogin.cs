namespace RedGate.SSC.Windows.Client.Credentials
{
    internal class SscLogin
    {
        private readonly string m_UserName;
        private readonly string m_Password;

        public SscLogin(string userName, string password)
        {
            m_UserName = userName;
            m_Password = password;
        }

        public string UserName
        {
            get { return m_UserName; }
        }

        public string Password
        {
            get { return m_Password; }
        }
    }
}
