using System;
using System.Drawing;
using RedGate.SIPFrameworkShared;

namespace RedGate.SSC.Windows.Host.OeContextMenus
{
    internal class DatabaseLevelMenuItem : ActionSimpleOeMenuItemBase
    {
        private readonly Action m_ExecuteAction;
        private static readonly Bitmap s_ProductImage = new Icon(Product.Resources.ProductIcon, 16, 16).ToBitmap();

        public DatabaseLevelMenuItem(Action executeAction)
        {
            m_ExecuteAction = executeAction;
        }

        public override Image ItemImage
        {
            get { return s_ProductImage; }
        }

        public override bool AppliesTo(ObjectExplorerNodeDescriptorBase oeNode)
        {
            var dbNode = oeNode as ObjectExplorerDatabaseNodeDescriptor;
            if (dbNode == null || dbNode.IsSystemNode) return false;
            return (!(oeNode is ObjectExplorerFolderNodeDescriptor) &&
                   !(oeNode is ObjectExplorerObjectNodeDescriptor) &&
                   !(oeNode is ObjectExplorerParameterNodeDescriptor) &&
                   !(oeNode is ObjectExplorerColumnNodeDescriptor));
        }

        public override string ItemText
        {
            get { return Product.Resources.BrowseContextMenu; }
        }

        public override void OnAction(ObjectExplorerNodeDescriptorBase node)
        {
            ObjectExplorerDatabaseNodeDescriptor dbNode = node as ObjectExplorerDatabaseNodeDescriptor;
            if (dbNode == null)
            {
                return;
            }

            m_ExecuteAction();
        }
    }
}
