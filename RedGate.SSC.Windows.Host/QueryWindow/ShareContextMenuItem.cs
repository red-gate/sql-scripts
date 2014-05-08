using System;
using log4net;
using RedGate.SIPFrameworkShared;

namespace RedGate.SSC.Windows.Host.QueryWindow
{
    public class ShareContextMenuItem : ISharedCommand
    {
        public string Name { get { return "Share"; } }
        public string Caption { get { return "Share to SQLServerCentral..."; } }
        public string Tooltip { get { return "Share this script to SQLServerCentral."; } }
        public ICommandImage Icon { get { return new CommandImageIcon(Product.Resources.ProductIcon); } }

        public string[] DefaultBindings { get {return new string[0];} }
        public bool Visible { get { return true; } }
        public bool Enabled { get { return true; } }

        public void Execute()
        {
            var browseScriptsPage = ObjectFactory.Get<IBrowseScriptsPage>();
            var queryWindowManager = ObjectFactory.Get<ISsmsQueryWindowManager>();

            string query = "Failed to copy text";
            try
            {
                query = queryWindowManager.GetActiveAugmentedQueryWindowContents();
            }
            catch (Exception e)
            {
                ObjectFactory.Get<ILog>().Error("Query window share context menu failed to get the window's content.", e);
            }
            
            browseScriptsPage.ShowShare(query);
        }
    }


}
