using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Threading;
using JetBrains.Annotations;
using RedGate.AppHost.Interfaces;
using RedGate.SSC.Windows.Analytics;
using RedGate.SSC.Windows.Logging;
using RedGate.SSC.Windows.Remote.Services;

namespace RedGate.SSC.Windows.Client
{
    [UsedImplicitly]
    public class OutOfProcessEntryPoint : IOutOfProcessEntryPoint
    {
        private BrowseScriptsPageControl m_BrowseScriptsPageControl;
        private ScreenSelectionNotifications m_ScreenSelectionNotifications;

        public FrameworkElement CreateElement(IAppHostServices service)
        {
            LogConfigurator.InitializeForChild();

            ObjectFactory.Bind<ApplicationDispatcher>().ToConstant(new ApplicationDispatcher(Dispatcher.CurrentDispatcher)).InSingletonScope();
            ObjectFactory.Bind<IAnalytics>().ToMethod(context => AnalyticsFactory.Create()).InSingletonScope();
            ObjectFactory.Bind<ISsmsOperations>().ToConstant(service.GetService<ISsmsOperations>()).InSingletonScope();
            
            m_BrowseScriptsPageControl = new BrowseScriptsPageControl();

            m_ScreenSelectionNotifications = new ScreenSelectionNotifications();
            m_ScreenSelectionNotifications.ShareScreenSelected +=
                (sender, args) => m_BrowseScriptsPageControl.ShowShare(args.ScriptBody);
            
            var callbacksRegistrationService = service.GetService<ICallbacksRegistrationService>();
            callbacksRegistrationService.Register(m_ScreenSelectionNotifications);
            
            return new WindowsFormsHost
                   {
                       Child = m_BrowseScriptsPageControl
                   };
        }
    }
}
