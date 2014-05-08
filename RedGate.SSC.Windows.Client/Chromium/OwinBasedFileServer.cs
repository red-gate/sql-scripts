using System;
using System.Net.Http;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.Testing;
using Owin;

namespace RedGate.SSC.Windows.Client.Chromium
{
    internal class OwinBasedFileServer
    {
        private readonly TestServer m_OwinTestServer;

        public OwinBasedFileServer(IFileSystem fileSystem)
        {
            if (fileSystem == null)
                throw new ArgumentNullException("fileSystem");

            m_OwinTestServer = TestServer.Create(app => app.UseFileServer(new FileServerOptions
                                                                          {
                                                                              EnableDefaultFiles = false,
                                                                              EnableDirectoryBrowsing = true,
                                                                              FileSystem = fileSystem
                                                                          }));
        }

        public HttpResponseMessage Request(string uri)
        {
            return m_OwinTestServer.HttpClient.GetAsync(uri).Result;
        }
    }
}