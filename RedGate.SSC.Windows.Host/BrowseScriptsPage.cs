using RedGate.SIPFrameworkShared;
using RedGate.SSC.Windows.Product;

namespace RedGate.SSC.Windows.Host
{
    internal class BrowseScriptsPage : IBrowseScriptsPage
    {
        private readonly RemoteBridge m_Bridge;
        private readonly ISsmsTabPage m_SsmsTabPage;
        
        public BrowseScriptsPage(ISsmsFunctionalityProvider6 provider, RemoteBridge bridge)
        {
            m_Bridge = bridge;
            m_SsmsTabPage = provider.CreateTabPage(typeof(BrowseScriptsPageControl), TheProduct.Name, TheProduct.MainPaneId);
        }

        public void Show()
        {
            m_SsmsTabPage.Activate();
        }

        public void ShowShare(string script)
        {
            m_SsmsTabPage.Activate();

            m_Bridge.ShowShareScreen(script);
        }
    }
}