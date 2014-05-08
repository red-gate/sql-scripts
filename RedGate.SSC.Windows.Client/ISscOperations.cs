using RedGate.SSC.Windows.Client.Chromium;
using RedGate.SSC.Windows.Client.Credentials;

namespace RedGate.SSC.Windows.Client
{
    internal interface ISscOperations : IBindableToJs
    {
        void OpenSscQuery(string queryId);

        bool HaveAuthenticationCredentials();

        string[] GetAuthenticationCredentials();

        void PromptForSscCredentials();

        bool DeleteAuthenticationCredentials();
        
        void SetFavorite(int contentItemId);
        

        /// <summary>
        /// JSON representation of a FavoriteScript[]
        /// </summary>
        /// <returns></returns>
        string GetInfoOnFavorites();
    }
}