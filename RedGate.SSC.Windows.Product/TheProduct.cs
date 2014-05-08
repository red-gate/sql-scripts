using System;
using System.IO;
using System.Reflection;

namespace RedGate.SSC.Windows.Product
{
    public static class TheProduct
    {
        public static string Copyright
        {
            get { return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright; }
        }

        public static string Name
        {
            get { return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title; }
        }

        public static Version Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        public static string CredentialsTarget 
        {
            get { return "SQLServerCentral for Windows"; }
        }

        public static string MainPaneId
        {
            get { return "{50757731-C111-486B-ADAF-E2997A1FBB5F}"; }
        }

        public static string MixPanelToken
        {
            get { return "b128acee140a624101fd4b45af5d2a8d"; }
        }

        public static string LogPath
        {
            get
            {
                string redgateAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Red Gate");
                string redgateLogs = Path.Combine(redgateAppData, "Logs");
                return Path.Combine(redgateLogs, Name);
            }
        }

        public static string InstallPath
        {
            get
            {
                string engineCodeBase = Assembly.GetExecutingAssembly().CodeBase;
                string enginePath = new Uri(engineCodeBase).LocalPath;
                FileInfo engineInfo = new FileInfo(enginePath);
                DirectoryInfo engineDirectory = engineInfo.Directory;

                if (engineDirectory == null)
                {
                    throw new Exception(String.Format("Unexpected null directory for {0}", enginePath));
                }
                else
                {
                    return engineDirectory.FullName;
                }
            }
        }

        public static string ProductApplicationData
        {
            get
            {
                string redgateApplicationData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Red Gate");

                string productApplicationData = Path.Combine(redgateApplicationData, Name);

                if (!Directory.Exists(productApplicationData))
                    Directory.CreateDirectory(productApplicationData);

                return productApplicationData;
            }
        }
    }
}
