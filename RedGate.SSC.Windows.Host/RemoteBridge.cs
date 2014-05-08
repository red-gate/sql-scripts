using System;
using System.Runtime.Remoting.Lifetime;
using System.Windows.Forms.Integration;
using System.Windows.Threading;
using JetBrains.Annotations;
using RedGate.AppHost.Interfaces;
using RedGate.AppHost.Remoting.WPF;
using RedGate.SIPFrameworkShared;
using RedGate.SSC.Windows.Remote.Services;

namespace RedGate.SSC.Windows.Host
{
    [UsedImplicitly]
    internal class RemoteBridge : MarshalByRefObject, ICallbacksRegistrationService, IScreenSelectionNotifications, ISsmsOperations
    {
        private readonly ISsmsQueryWindowManager m_QueryWindowServices;
        private readonly Dispatcher m_Dispatcher;
        private IScreenSelectionNotifications m_ScreenSelectionCallbacks;

        public RemoteBridge(ISsmsQueryWindowManager queryWindowServices, Dispatcher dispatcher)
        {
            m_QueryWindowServices = queryWindowServices;
            m_Dispatcher = dispatcher;
        }

        public void Register(IScreenSelectionNotifications screenSelection)
        {
            m_ScreenSelectionCallbacks = screenSelection;
        }

        public void ShowShareScreen(string script)
        {
            m_ScreenSelectionCallbacks.ShowShareScreen(script);
        }

        public void CreateAugmentedQueryWindow(string sqlScript, string title, IRemoteElement remoteElement)
        {
            m_Dispatcher.BeginInvoke(new Action(() => CreateAugmentedQueryWindowInner(sqlScript, title, remoteElement)));
        }

        private void CreateAugmentedQueryWindowInner(string sqlScript, string title, IRemoteElement remoteElement)
        {
            m_QueryWindowServices.CreateAugmentedQueryWindow(sqlScript, title, new ElementHost
                                                                               {
                                                                                   Child =
                                                                                       remoteElement.ToFrameworkElement(),
                                                                                       Height = 52
                                                                               });
        }

        public TimeSpan Renewal(ILease lease)
        {
            return TimeSpan.FromMinutes(1);
        }

        public override object InitializeLifetimeService()
        {
            ILease ret = (ILease)base.InitializeLifetimeService();
            ret.SponsorshipTimeout = TimeSpan.FromMinutes(2);
            ret.Register(this);
            return ret;
        }
    }
}