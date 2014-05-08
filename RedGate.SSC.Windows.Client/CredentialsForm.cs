using System.Drawing;
using System.Windows.Forms;
using RedGate.SSC.Windows.Client.Chromium;
using RedGate.SSC.Windows.Client.JavaScriptModels;

namespace RedGate.SSC.Windows.Client
{
    internal class CredentialsForm : Form
    {
        public CredentialsForm()
        {
            Controls.Add(new ChromiumControl("http://localhost:1337/login.html", new CredentialsFormViewModel(), new SscEndpoints())
                             {
                                 Dock = DockStyle.Fill
                             });
            
            Text = Product.Resources.CredentialsFormTitle;
            Icon = Product.Resources.ProductIcon;
            Size = new Size(820, 420);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
        }
    }
}