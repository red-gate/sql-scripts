using System;
using System.Runtime.Remoting.Lifetime;
using RedGate.SSC.Windows.Remote.Services;

namespace RedGate.SSC.Windows.Client
{
    internal class ScreenSelectionNotifications : MarshalByRefObject, IScreenSelectionNotifications
    {
        internal event EventHandler<ShareScreenSelectionEventArgs>  ShareScreenSelected;

        public void ShowShareScreen(string script)
        {
            OnShareScreenSelected(script);
        }

        private void OnShareScreenSelected(string script)
        {
            var handler = ShareScreenSelected;
            if (handler != null) handler(this, new ShareScreenSelectionEventArgs(script));
        }

        public TimeSpan Renewal(ILease lease)
        {
            return TimeSpan.FromMinutes(1);
        }

        public override object InitializeLifetimeService()
        {
            ILease ret = (ILease)base.InitializeLifetimeService();
            ret.SponsorshipTimeout = TimeSpan.FromMinutes(2);
            ret.Register(this);
            return ret;
        }
    }
}