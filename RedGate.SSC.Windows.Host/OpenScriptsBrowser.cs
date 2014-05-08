using System;
using RedGate.SIPFrameworkShared;

namespace RedGate.SSC.Windows.Host
{
    internal class OpenScriptsBrowser : ISharedCommand
    {
        private readonly Action m_ExecuteAction;

        public OpenScriptsBrowser(Action executeAction)
        {
            m_ExecuteAction = executeAction;
        }

        public string Caption
        {
            get { return Product.Resources.ToolbarButtonText; }
        }

        public void Execute()
        {
            m_ExecuteAction();
        }

        public string Name
        {
            get { return Product.Resources.ToolbarButtonName; }
        }

        public string Tooltip
        {
            get { return Product.Resources.ToolbarButtonToolTip; }
        }

        public ICommandImage Icon { get { return new CommandImageIcon(Product.Resources.ProductIcon); } }

        public string[] DefaultBindings { get { return new string[0]; } }

        public bool Visible { get { return true; } }

        public bool Enabled { get { return true; } }
    }
}