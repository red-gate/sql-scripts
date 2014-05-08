using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using RedGate.AppHost.Server;
using RedGate.SSC.Windows.Remote.Services;

namespace RedGate.SSC.Windows.Host
{
    [ComVisible(true)]
    public class BrowseScriptsPageControl : UserControl
    {
        private readonly RemoteBridge m_RemoteBridge;
        
        public BrowseScriptsPageControl()
        {
            m_RemoteBridge = ObjectFactory.Get<RemoteBridge>();
            
            var appHostChildHandle = new ChildProcessFactory().Create("RedGate.SSC.Windows.Client.dll", Debugger.IsAttached);

            AppHostServices appHostServices = new AppHostServices();
            appHostServices.RegisterService<RemoteBridge, ICallbacksRegistrationService>(m_RemoteBridge);
            appHostServices.RegisterService<RemoteBridge, ISsmsOperations>(m_RemoteBridge);

            var element = appHostChildHandle.CreateElement(appHostServices);

            Controls.Add(new ElementHost
                         {
                             Dock = DockStyle.Fill,
                             Child = element
                         });
        }
    }
}