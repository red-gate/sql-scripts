using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RedGate.SSC.Windows.Client.Chromium;
using RedGate.SSC.Windows.Client.Credentials;
using RedGate.SSC.Windows.Client.JavaScriptModels;

namespace RedGate.SSC.Windows.Client
{
    [ComVisible(true)]
    public class BrowseScriptsPageControl : UserControl
    {
        private readonly ChromiumControl m_ChromiumControl;
        private readonly IScriptSnippets m_ScriptSnippets = ObjectFactory.Get<IScriptSnippets>();
        private readonly ICredentialsManager m_CredentialsManager = ObjectFactory.Get<ICredentialsManager>();

        public BrowseScriptsPageControl()
        {
            m_ChromiumControl = new ChromiumControl("index.html", ObjectFactory.Get<ISscOperations>(), m_ScriptSnippets, new SscEndpoints())
                {
                    Dock = DockStyle.Fill
                };
            Controls.Add(m_ChromiumControl);

            m_CredentialsManager.Changed += OnCredentialsChanged;
        }

        //a hack to update the views as we can't have ShowDialog() that would cause it to block and allow us to use callbacks
        private void OnCredentialsChanged(object sender, EventArgs eventArgs)
        {
            //TODO: Update the Angular app state. Use signalr?
            m_ChromiumControl.EvaluateJavaScript("window.location.reload()");
        }

        public void ShowShare(string script)
        {
            var snippetId  = m_ScriptSnippets.RegisterScriptSnippet(Guid.NewGuid(), script);
            m_ChromiumControl.NavigateToUrl(string.Format("contribute/{0}", snippetId));
        }
    }
}