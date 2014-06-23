using System.Drawing;
using System.Windows.Forms;
using RedGate.SSC.Windows.Client.Chromium;
using RedGate.SSC.Windows.Client.JavaScriptModels;

namespace RedGate.SSC.Windows.Client
{
    class QueryWindowHeader : UserControl
    {
        public QueryWindowHeader(ScriptItem scriptItem)
            : this(ObjectFactory.Get<ISscOperations>(), scriptItem)
        {
            InitializeComponent();
        }

        private QueryWindowHeader(ISscOperations sscOperations, ScriptItem scriptItem)
        {
            Controls.Add(new ChromiumControl("query-header.html", sscOperations, scriptItem, new SscEndpoints())
            {
                Dock = DockStyle.Fill
            });

            Height = 52;
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            Size = new Size(150, 52);
            ResumeLayout(false);
            MaximumSize = new Size(5000, 52);
        }
    }
}
