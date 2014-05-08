using System;
using RedGate.SSC.Windows.Client.Chromium;

namespace RedGate.SSC.Windows.Client
{
    internal interface IScriptSnippets : IBindableToJs
    {
        Guid RegisterScriptSnippet(Guid guid, string script);
        
        string GetSnippet(string guid);
    }
}