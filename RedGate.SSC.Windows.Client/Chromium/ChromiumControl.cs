using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;
using CefSharp;
using CefSharp.WinForms;
using log4net;
using RedGate.SSC.Windows.Client.EmbeddedResourceFileSystem;
using RedGate.SSC.Windows.Web.Assets;

namespace RedGate.SSC.Windows.Client.Chromium
{
    [ComVisible(true)]
    public class ChromiumControl : UserControl, IMenuHandler
    {
        private const string c_InternalDomain = @"http://localhost:1337";

        private readonly ILog m_Log;
        private readonly WebView m_WebView;

        public ChromiumControl(string address, params IBindableToJs[] objectsToBind)
        {
            address = AddInternalDomain(address);
            m_Log = ObjectFactory.Get<ILog>();
            CEF.Initialize(new Settings());

            m_WebView = new WebView(address, new BrowserSettings
                                             {
                                                 WebSecurityDisabled = true
                                             })
                          {
                              Dock = DockStyle.Fill,
                              RequestHandler = new InterceptingRequestHandler(c_InternalDomain, new OwinBasedFileServer(new EmbeddedResourceFileSystemWithDirectorySupport(AssetsAssembly.Get())).Request)
                          };

            foreach (var toBind in objectsToBind)
            {
                m_WebView.RegisterJsObject(toBind.Name, toBind);
            }

            m_WebView.PropertyChanged += (sender, args) =>
                                         {
                                             m_WebView.Address = address;

                                            if (args.PropertyName == "IsBrowserInitialized" && Keyboard.IsKeyDown(Key.LeftShift))
                                            {
                                                
                                                m_WebView.ShowDevTools();
                                            }
                                        };
            m_WebView.ConsoleMessage += LogConsoleMessage;
            m_WebView.LifeSpanHandler = new PopupHandler();

            // Without this:
            //  The webview isn't initialized for a while after the tab is shown, so we have to wait, polling it.
            //  About 1/3 times when the tab gets created the webview only fills the top left hand corner of it.
            SizeChanged += ReinitializeAndResize;

            Controls.Add(m_WebView);

            m_WebView.MenuHandler = this;
        }

        private string AddInternalDomain(string address)
        {
            return string.Format(@"{0}/{1}", c_InternalDomain, address);
        }

        private void LogConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            m_Log.WarnFormat("{0} at {1}:{2}", e.Message, new Uri(e.Source).LocalPath.TrimStart('/'), e.Line);
        }

        private void ReinitializeAndResize(object o, EventArgs a)
        {
            if (m_WebView.IsHandleCreated)
            {
                ReinitializeAndResizeInner(o,a);
            }
            else
            {
                m_WebView.HandleCreated += ReinitializeAndResizeInner;
            }
        }

        private void ReinitializeAndResizeInner(object o, EventArgs a)
        {
            //This method is oddly named but seems to work.
            m_WebView.OnInitialized();
        }

        /// <summary>
        /// The tab must be made visible before calling this method.
        /// </summary>
        /// <param name="url"></param>
        public void NavigateToUrl(string url)
        {
            m_WebView.Load(AddInternalDomain(url));
        }

        protected override void Dispose(bool disposing)
        {
            SizeChanged -= ReinitializeAndResize;
            m_WebView.HandleCreated -= ReinitializeAndResizeInner;
            base.Dispose(disposing);
        }

        internal void EvaluateJavaScript(string script)
        {
            m_WebView.EvaluateScript(script);
        }

        public bool OnBeforeMenu(IWebBrowser browser)
        {
            return !Keyboard.IsKeyDown(Key.LeftShift);
        }
    }
}