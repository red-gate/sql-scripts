using System;
using System.IO;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using RedGate.SSC.Windows.Product;

namespace RedGate.SSC.Windows.Logging
{
    public static class LogConfigurator
    {
        public static void InitializeForChild()
        {
            ConfigureLogging("LogConfiguration.config", "child-log.txt");
        }

        public static void InitializeForHost()
        {
            ConfigureLogging("LogConfiguration.config", "host-log.txt");
        }

        /// <summary>
        /// Both parameters are simple filenames, not paths
        /// </summary>
        private static void ConfigureLogging(string loggingConfigFilename, string defaultLogFilename)
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders();
            
            var loggingConfigFile = new FileInfo(loggingConfigFilename);
            
            if (loggingConfigFile.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(loggingConfigFile);
            }
            else
            {
                ConfigureDefaultLogging(Path.Combine(TheProduct.LogPath, defaultLogFilename));
            }
#if DEBUG
            var consoleAppender = new TraceAppender
                {
                    Layout = DefaultPatternLayout(false), 
                    Threshold = Level.All, 
                    ImmediateFlush = true
                };
            consoleAppender.ActivateOptions();
            BasicConfigurator.Configure(consoleAppender);
#endif
        }

        private static void ConfigureDefaultLogging(string loggingOutputFile)
        {
            var fileAppender = new FileAppender
                                   {
                                       AppendToFile = true,
                                       LockingModel = new FileAppender.MinimalLock(),
                                       File = loggingOutputFile,
                                       Layout = DefaultPatternLayout(true),
                                       Threshold = Level.Warn
                                   };
            fileAppender.ActivateOptions();
            BasicConfigurator.Configure(fileAppender);
        }

        private static PatternLayout DefaultPatternLayout(bool withFullContext)
        {
            var timeThreadLevelModuleContext = withFullContext ? "%d [%2%t] %-5p [%-10c] " : String.Empty;
            var pl = new PatternLayout
                {
                    ConversionPattern = String.Format("{0}%m%n%n", timeThreadLevelModuleContext)
                };
            pl.ActivateOptions();
            return pl;
        }
    }
}