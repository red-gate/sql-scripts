using log4net;
using Ninject.Activation;
using Ninject.Modules;

namespace RedGate.SSC.Windows.Host
{
    internal class HostModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(GetLog);
            Bind<IBrowseScriptsPage>().To<BrowseScriptsPage>().InSingletonScope();
            Bind<RemoteBridge>().ToSelf().InSingletonScope();
        }

        private ILog GetLog(IContext arg)
        {
            if (arg.Request.Target == null) return LogManager.GetLogger(GetType());
            return LogManager.GetLogger(arg.Request.Target.Member.DeclaringType);
        }
    }
}