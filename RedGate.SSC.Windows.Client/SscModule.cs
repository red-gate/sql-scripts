using Ninject.Activation;
using Ninject.Modules;
using RedGate.SSC.Windows.Client.Credentials;
using log4net;
using RedGate.SSC.Windows.Client.JavaScriptModels;

namespace RedGate.SSC.Windows.Client
{
    internal class SscModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(GetLog);
            Bind<IScriptSnippets>().To<ScriptSnippets>();
            Bind<ISscOperations>().To<SscOperations>().InSingletonScope();
            Bind<ICredentialsManager>().To<WindowsCryptStoreCredentialsManager>().InSingletonScope();
            Bind<IDialogController>().To<DialogController>().InSingletonScope();
            Bind<IFavoriteScriptsStore>().To<FavoriteScriptsStore>().InSingletonScope();
        }

        private ILog GetLog(IContext arg)
        {
            if (arg.Request.Target == null) return LogManager.GetLogger(GetType());
            return LogManager.GetLogger(arg.Request.Target.Member.DeclaringType);
        }
    }
}