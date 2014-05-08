using System;
using System.Windows.Threading;

namespace RedGate.SSC.Windows.Client
{
    public class ApplicationDispatcher
    {
        private readonly Dispatcher m_CurrentDispatcher;

        public ApplicationDispatcher(Dispatcher currentDispatcher)
        {
            m_CurrentDispatcher = currentDispatcher;
        }

        internal void Invoke(Action action)
        {
            if (m_CurrentDispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                m_CurrentDispatcher.Invoke(action);
            }
        }
    }
}