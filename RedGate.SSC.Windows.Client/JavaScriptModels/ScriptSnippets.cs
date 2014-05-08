using System;
using System.Collections.Generic;

namespace RedGate.SSC.Windows.Client.JavaScriptModels
{
    internal class ScriptSnippets : IScriptSnippets
    {
        private readonly Dictionary<Guid, string> m_Snippets = new Dictionary<Guid, string>();

        public Guid RegisterScriptSnippet(Guid guid, string script)
        {
            m_Snippets.Add(guid, script);
            return guid;
        }

        public string GetSnippet(string guid)
        {
            return GetSnippet(Guid.Parse(guid));
        }

        private string GetSnippet(Guid guid)
        {
            return m_Snippets[guid];
        }

        public string Name
        {
            get { return "RedGate_SSC_Windows_Client_ScriptSnippets"; }
        }
    }
}