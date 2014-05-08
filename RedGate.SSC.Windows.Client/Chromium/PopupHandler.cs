using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using log4net;
using Microsoft.Win32;

namespace RedGate.SSC.Windows.Client.Chromium
{
    internal class PopupHandler : ILifeSpanHandler
    {
        public static readonly ILog s_Log = ObjectFactory.Get<ILog>();

        public bool OnBeforePopup(IWebBrowser browser, string url, ref int x, ref int y, ref int width, ref int height)
        {
            if (url.StartsWith("chrome-devtools", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            OpenWebPage(url);
            return true;
        }

        public void OnBeforeClose(IWebBrowser browser)
        {
            
        }

        private static void OpenWebPage(string url)
        {
            //Check for default browser
            if (!IsDefaultBrowserRegistered() || !OpenBrowser(url))
            {
                MessageBox.Show(String.Format(Product.Resources.CantFindBrowserMessage, url), Product.Resources.CantFindBrowserTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static bool OpenBrowser(string url)
        {
            //see http://blogs.msdn.com/b/oldnewthing/archive/2004/09/01/223936.aspx
            ProcessStartInfo psi = new ProcessStartInfo
            {
                Verb = "open",
                FileName = url
            };

            try
            {
                using (Process.Start(psi))
                {
                }

                return true;
            }
            catch (Exception e)
            {
                s_Log.Error("Couldn't open web page " + url, e);
                return false;
            }   
        }

        private static bool IsDefaultBrowserRegistered()
        {
            try
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false))
                {
                    if (key == null)
                        return false;

                    string browser = key.GetValue(null, String.Empty).ToString().ToLower().Trim('"');

                    if (!browser.Contains(".exe"))
                        return false;

                    if (!browser.EndsWith("exe"))
                    {
                        //get rid of everything after the ".exe"
                        browser = browser.Substring(0, browser.LastIndexOf(".exe", StringComparison.OrdinalIgnoreCase) + 4);
                    }

                    if (!File.Exists(browser))
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}