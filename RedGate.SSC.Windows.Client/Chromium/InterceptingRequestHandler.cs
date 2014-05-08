using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using CefSharp;

namespace RedGate.SSC.Windows.Client.Chromium
{
    internal class InterceptingRequestHandler : IRequestHandler
    {
        private readonly string m_InternalDomain;
        private readonly Func<string, HttpResponseMessage> m_Server;

        public InterceptingRequestHandler(string internalDomain, Func<string, HttpResponseMessage> server)
        {
            m_InternalDomain = internalDomain;
            m_Server = server;
        }

        public bool OnBeforeBrowse(IWebBrowser browser, IRequest request, NavigationType naigationvType, bool isRedirect)
        {
            return false;
        }

        public bool OnBeforeResourceLoad(IWebBrowser browser, IRequestResponse requestResponse)
        {
            /*
            * Called on the IO thread before a resource is loaded. To allow the resource to load normally return false. 
            * To redirect the resource to a new url populate the |redirectUrl| value and return false. 
            * To specify data for the resource return a CefStream object in |resourceStream|, use the |response| object to set mime type, 
            * HTTP status code and optional header values, and return false. To cancel loading of the resource return true.
            * Any modifications to |request| will be observed. If the URL in |request| is changed and |redirectUrl| is also set,
            * the URL in |request| will be used.
            */
            if (requestResponse.Request.Url.StartsWith(m_InternalDomain))
            {
                var requestUri = requestResponse.Request.Url.Replace(m_InternalDomain, String.Empty);

                HttpResponseMessage response = m_Server(requestUri);

                //TODO: Copy to separate memory stream so we can dispose of parent HttpResponseMessage
                var responseContent = response.Content.ReadAsStreamAsync().Result;

                var responseHeaders = response.Headers.ToDictionary(x => x.Key, x => x.Value.First());

                var responseMime = response.IsSuccessStatusCode
                    ? response.Content.Headers.ContentType.MediaType
                    : "text/html"; //CEFSharp demands a MimeType of some kind...

                requestResponse.RespondWith(responseContent, responseMime, String.Empty, (int) response.StatusCode, responseHeaders);

            }

            return false;
        }

        public void OnResourceResponse(IWebBrowser browser, string url, int status, string statusText, string mimeType,
            WebHeaderCollection headers)
        {
        }

        public bool GetDownloadHandler(IWebBrowser browser, string mimeType, string fileName, long contentLength,
            ref IDownloadHandler handler)
        {
            return false;
        }

        public bool GetAuthCredentials(IWebBrowser browser, bool isProxy, string host, int port, string realm, string scheme,
            ref string username, ref string password)
        {
            return false;
        }
    }
}