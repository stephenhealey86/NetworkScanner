using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScanner
{
    public class LoggingService : ILoggingService
    {
        #region Private Variables
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        public LoggingService()
        {
            ConfigureLogger();
        }
        #endregion

        #region Methods
        public void Log(LogLevels logLevel, string message)
        {
            switch (logLevel)
            {
                case LogLevels.Trace:
                    Logger.Trace(message);
                    break;
                case LogLevels.Debug:
                    Logger.Debug(message);
                    break;
                case LogLevels.Info:
                    Logger.Info(message);
                    break;
                case LogLevels.Warn:
                    Logger.Warn(message);
                    break;
                case LogLevels.Error:
                    Logger.Error(message);
                    break;
                case LogLevels.Fatal:
                    Logger.Fatal(message);
                    break;
            }
        }

        private void ConfigureLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "./Logs/log.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;
        }
        #endregion
    }

    public enum LogLevels
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
}
