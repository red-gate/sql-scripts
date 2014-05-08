using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedGate.AppHost.Remoting.WPF;
using RedGate.SSC.Windows.Client.Credentials;
using RedGate.SSC.Windows.Remote.Services;

namespace RedGate.SSC.Windows.Client.JavaScriptModels
{
    internal class SscOperations : ISscOperations
    {
        private readonly ISsmsOperations m_SsmsOperations;
        private readonly ApplicationDispatcher m_Dispatcher;
        private readonly ICredentialsManager m_CredentialsManager;
        private readonly IDialogController m_DialogController;
        private readonly SscEndpoints m_SscEndpoints;
        private readonly IFavoriteScriptsStore m_FavoriteStore;

        public SscOperations(ISsmsOperations ssmsOperations,
            ApplicationDispatcher dispatcher, 
            ICredentialsManager credentialsManager,
            IDialogController dialogController,
            SscEndpoints sscEndpoints,
            IFavoriteScriptsStore favoriteStore)
        {
            m_SsmsOperations = ssmsOperations;
            m_Dispatcher = dispatcher;
            m_CredentialsManager = credentialsManager;
            m_DialogController = dialogController;
            m_SscEndpoints = sscEndpoints;
            m_FavoriteStore = favoriteStore;

            
#if DEBUG
            //Trust all SSL issues when debugging. Obviously this is terrible as we are now susceptible to MITM attacks.
            ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
#endif
        }

        public void OpenSscQuery(string queryId)
        {
            Task<ScriptItem> t = Task.Factory.StartNew(() => GetScriptItem(queryId));

            t.ContinueWith(task => m_Dispatcher.Invoke(() =>
                                                       {
                                                           QueryWindowHeader queryWindowHeader = new QueryWindowHeader(task.Result);

                                                           var windowsFormsHost = new WindowsFormsHost
                                                                                               {
                                                                                                   Child = queryWindowHeader,
                                                                                                   Height = queryWindowHeader.Height
                                                                                               }.ToRemotedElement();

                                                           m_SsmsOperations.CreateAugmentedQueryWindow(task.Result.SqlScript, task.Result.Title, windowsFormsHost);
                                                       }));
        }

        public bool HaveAuthenticationCredentials()
        {
            SscLogin password;
            return m_CredentialsManager.TryReadCred(out password);
        }

        public string[] GetAuthenticationCredentials()
        {
            SscLogin login;
            if (!m_CredentialsManager.TryReadCred(out login))
                throw new InvalidOperationException("No credentials stored for this user, call PromptForCredentials first");

            return new[] {login.UserName, login.Password};
        }

        public bool DeleteAuthenticationCredentials()
        {
            return m_CredentialsManager.Delete();
        }

        public void PromptForSscCredentials()
        {
            m_DialogController.PromptAndStoreCredentials();
        }

        private ScriptItem GetScriptItem(string queryId)
        {
            string stringResult = new WebClient().DownloadString(m_SscEndpoints.GetSqlScriptEndpoint(Int32.Parse(queryId)));

            JToken jToken = JObject.Parse(stringResult)["Script"];

            return jToken.ToObject<ScriptItem>();
        }

        public void SetFavorite(int contentItemId)
        {
            m_FavoriteStore.AddOrUpdateFavoriteDate(contentItemId);
        }

        public string GetInfoOnFavorites()
        {
            return JsonConvert.SerializeObject(m_FavoriteStore.Favorites.ToDictionary(x => x.ContentItemId, x => x.FavoritedDate));
        }

        public string Name
        {
            get { return "RedGate_SSC_Windows_Client_SscOperations"; }
        }
    }
}