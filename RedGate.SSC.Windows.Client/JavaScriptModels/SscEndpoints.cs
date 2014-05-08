using System;
using RedGate.SSC.Windows.Client.Chromium;

namespace RedGate.SSC.Windows.Client.JavaScriptModels
{
    internal class SscEndpoints : IBindableToJs
    {
        private const string c_ApiRoot = "https://www.sqlservercentral.com";

        public string GetSqlScriptEndpoint(int scriptId)
        {
            return String.Format("{0}/api/scripts/{1}", c_ApiRoot, scriptId);
        }

        public string GetIsAuthenticatedEndpoint()
        {
            return String.Format("{0}/api/authenticate", c_ApiRoot);
        }

        public string GetFavoriteScriptsEndpoint()
        {
            return String.Format("{0}/api/briefcase/scripts", c_ApiRoot);
        }

        public string GetAddToFavoriteScriptsEndpoint()
        {
            return String.Format("{0}/api/briefcase", c_ApiRoot);
        }

        public string GetShareEndpoint()
        {
            return String.Format("{0}/api/scripts", c_ApiRoot);
        }

        public string GetListScriptsEndpoint()
        {
            return String.Format("{0}/api/scripts?s=", c_ApiRoot);
        }

        public string Name
        {
            get { return "SscEndpoints"; }
        }
    }
}