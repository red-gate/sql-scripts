using System;
using System.Linq;
using Ninject;
using Ninject.Syntax;

namespace RedGate.SSC.Windows.Host
{
    internal static class ObjectFactory
    {
        private static readonly IKernel s_Kernel = new StandardKernel(new NinjectSettings
                                                                          {
                                                                              InjectNonPublic = true
                                                                          }, new HostModule());

        public static T Get<T>()
        {
            return s_Kernel.Get<T>();
        }

        public static IBindingToSyntax<T> Bind<T>()
        {
            if (s_Kernel.GetBindings(typeof(T)).Any())
            {
                throw new InvalidOperationException(String.Format("Binding already exists for type [{0}]", typeof(T)));
            }

            return s_Kernel.Bind<T>();
        }
    }
}