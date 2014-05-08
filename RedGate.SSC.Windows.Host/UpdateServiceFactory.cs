using System;
using System.Linq;
using System.Reflection;
using RedGate.SSC.Windows.Product;
using RedGate.SSC.Windows.Update.Interfaces;

namespace RedGate.SSC.Windows.Host
{
    internal class UpdateServiceFactory
    {
        internal bool TryCreate(string updateAsmPath, out ICheckForUpdatesService updateService)
        {
            Assembly updateAsm = Assembly.LoadFrom(updateAsmPath);

            var serviceFactoryType = updateAsm.GetTypes().SingleOrDefault(x => typeof (ICheckForUpdateServiceFactory).IsAssignableFrom(x));

            if (serviceFactoryType == null)
            {
                updateService = null;
                return false;
            }

            var serviceFactory = (ICheckForUpdateServiceFactory) Activator.CreateInstance(serviceFactoryType);

            updateService = serviceFactory.Create(TheProduct.Name, TheProduct.Version, Product.Resources.ProductIcon);
            return true;
        }
    }
}