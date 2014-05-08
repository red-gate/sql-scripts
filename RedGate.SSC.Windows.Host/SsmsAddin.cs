using System;
using System.IO;
using System.Windows.Threading;
using JetBrains.Annotations;
using RedGate.SIPFrameworkShared;
using RedGate.SSC.Windows.Host.OeContextMenus;
using RedGate.SSC.Windows.Host.QueryWindow;
using RedGate.SSC.Windows.Logging;
using RedGate.SSC.Windows.Product;
using log4net;
using RedGate.SSC.Windows.Update.Interfaces;

namespace RedGate.SSC.Windows.Host
{
    [UsedImplicitly]
    public class SsmsAddin : ISsmsAddin4
    {
        private static readonly ILog s_Logger = ObjectFactory.Get<ILog>();

        private ICheckForUpdatesService m_CheckForUpdatesService;
        
        public void OnLoad(ISsmsExtendedFunctionalityProvider provider)
        {
            AssemblyLoadHandler.Initialize();
            LogConfigurator.InitializeForHost();

            var ssmsProvider = (ISsmsFunctionalityProvider6) provider;
            var ssmsQueryWindowManager = ssmsProvider.GetQueryWindowManager();
            m_CheckForUpdatesService = GetBestCheckForUpdateService();

            ObjectFactory.Bind<ISsmsFunctionalityProvider6>().ToConstant(ssmsProvider);
            ObjectFactory.Bind<ISsmsQueryWindowManager>().ToConstant(ssmsQueryWindowManager);
            ObjectFactory.Bind<Dispatcher>().ToConstant(Dispatcher.CurrentDispatcher).InSingletonScope();
            
            AddObjectExplorerMenuItems(ssmsProvider);
            AddQueryWindowContextItems(ssmsQueryWindowManager);
            AddToolbarItems(ssmsProvider);
            AddToolsMenuItems(ssmsProvider);

            m_CheckForUpdatesService.OnStartup();

            s_Logger.Debug("Loaded");
        }

        private static ICheckForUpdatesService GetBestCheckForUpdateService()
        {
            string updateAssembly = Path.Combine(TheProduct.InstallPath, "RedGate.SSC.Windows.Update.dll");

            ICheckForUpdatesService service;
            if (File.Exists(updateAssembly) && new UpdateServiceFactory().TryCreate(updateAssembly, out service))
            {
                return service;
            }
            else
            {
                s_Logger.Debug("Can't find the Red Gate implementation of check for updates. Not looking for another");

                return new DisabledCheckForUpdateService();
            }
        }

        private void ShowBrowseScriptsPage()
        {
            var browseScriptsPage = ObjectFactory.Get<IBrowseScriptsPage>();
            browseScriptsPage.Show();

            m_CheckForUpdatesService.OnInteraction();
        }

        private void AddObjectExplorerMenuItems(ISsmsFunctionalityProvider6 ssmsProvider)
        {
            ssmsProvider.AddTopLevelMenuItem(new DatabaseLevelMenuItem(ShowBrowseScriptsPage));
        }

        private void AddToolbarItems(ISsmsFunctionalityProvider6 ssmsFunctionalityProvider)
        {
            ssmsFunctionalityProvider.AddToolbarItem(new OpenScriptsBrowser(ShowBrowseScriptsPage));
        }

        private void AddQueryWindowContextItems(ISsmsQueryWindowManager ssmsQueryWindowManager)
        {
            ssmsQueryWindowManager.AddQueryWindowContextMenuItem("Edit.Paste", new ShareContextMenuItem());
        }

        private void AddToolsMenuItems(ISsmsFunctionalityProvider6 ssmsFunctionalityProvider)
        {
            ssmsFunctionalityProvider.AddToolsMenuItem(new OpenScriptsBrowser(ShowBrowseScriptsPage));
        }

        public void OnLoad(ISsmsFunctionalityProvider provider)
        {
            OnLoad((ISsmsExtendedFunctionalityProvider)provider);
        }

        public void OnNodeChanged(ObjectExplorerNodeDescriptorBase node)
        {
        }

        public string Version
        {
            get
            {
                Version version = TheProduct.Version;
                return string.Format("{0} {1}.{2}", TheProduct.Name, version.Major, version.Minor);
            }
        }

        public string Description { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public string Url { get; private set; }
        public void OnShutdown()
        {
        }
    }
}
