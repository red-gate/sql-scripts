using System;

namespace RedGate.SSC.Windows.Client
{
    internal class ShareScreenSelectionEventArgs : EventArgs
    {
        private readonly string m_ScriptBody;

        public ShareScreenSelectionEventArgs(string scriptBody)
        {
            m_ScriptBody = scriptBody;
        }

        public string ScriptBody
        {
            get { return m_ScriptBody; }
        }
    }
}