using System;
using System.IO;
using System.Linq;
using System.Reflection;
using RedGate.SSC.Windows.Product;

namespace RedGate.SSC.Windows.Host
{
    /// <summary>
    /// This class exists to 'fix up' the load context of the assemblies loaded within SSMS> If an assembly can't be found, try and get it into 
    /// the right context or load it from disk.
    /// </summary>
    internal static class AssemblyLoadHandler
    {
        private static readonly string s_InstallDir = TheProduct.InstallPath;
        
        internal static void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
        }

        private static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName assemblyName = new AssemblyName(args.Name);

            //All our assemblies have versions, don't use Xml Serializers and aren't the blacklist
            if (assemblyName.Version == null || assemblyName.Name.EndsWith(".XmlSerializers") || assemblyName.Name == "SqlWorkbench.Interfaces")
            {
                return null;
            }

            //Check if it already exists in the LoadFrom context
            var candidate = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.FullName == args.Name);

            //Try and load from our install directory if it wasn't already loaded
            if (candidate != null || TryLoadFromIfExists(s_InstallDir, assemblyName, out candidate))
                return candidate;
            
            return null;
        }

        private static bool TryLoadFromIfExists(string dir, AssemblyName requestedAssemblyName, out Assembly assembly)
        {
            string fileName = Path.Combine(dir, requestedAssemblyName.Name + ".dll");
            if (File.Exists(fileName))
            {
                Assembly candidate = Assembly.LoadFrom(fileName);

                AssemblyName candidateName = candidate.GetName();

                if (candidateName.FullName == requestedAssemblyName.FullName)
                {
                    assembly = candidate;
                    return true;
                }
            }

            assembly = null;
            return false;
        }
    }
}
