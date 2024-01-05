using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Lib.Extension;
using Clio.Demo.Core.Lib.Extension;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;


namespace Clio.Demo.Core.Lib.Util
{
    public static class Serilog
    {
        #region constants

        private const string Logo        = "Clio";
        private const string Folder      = "Logs";
        private const string ConfigKey   = "ApplicationInsights:InstrumentationKey";
        private const string LogTemplate = "{Timestamp:HH:mm:ss.fff} {CorrelationId} {Level:u4} {Username} {Message:lj}{NewLine}";

        #endregion constants

        #region setup logger(s)

        public static ILogger _fileLogger;
        public static ILogger _insightsLogger;
        public static ILogger _consoleLogger;

        private static ILog LogMaster { get; set; }

        public static void Initialize(ILog logMaster = null)
        {
            LogMaster = logMaster;

            _fileLogger    = new LoggerConfiguration().MinimumLevel.Is(LogEventLevel.Debug)
                                                      .WriteTo.File(path: $"{Folder}/{DT.Date}/{Logo}-{DT.Time}.log", outputTemplate: LogTemplate)
                                                      .CreateLogger();

            _consoleLogger = new LoggerConfiguration().MinimumLevel.Is(LogEventLevel.Debug)
                                                      .WriteTo.Console(theme: CustomConsoleThemes.Pro, outputTemplate: LogTemplate)
                                                      .CreateLogger();

            string instrumentationKey = LogMaster?.Configuration[ConfigKey];

            if (instrumentationKey.IsNotEmpty())
            {
                _insightsLogger = new LoggerConfiguration().MinimumLevel.Is(LogEventLevel.Information)
                                                           .WriteTo.ApplicationInsights(new TelemetryConfiguration { InstrumentationKey = instrumentationKey }, TelemetryConverter.Traces)
                                                           .CreateLogger();
            }
        }

        #endregion setup logger(s)

        public static void Debug(object sender, string message, params object[] args)
        {
            message = LogUtil.log(sender, message);

            _consoleLogger?.TextToConsole(LogEventLevel.Debug, message, args);
            _fileLogger   ?.Debug(message);
        }

        public static void Info(object sender, string message, params object[] args)
        {
            message = LogUtil.log(sender, message);

            _consoleLogger ?.TextToConsole(LogEventLevel.Information, message, args);
            _insightsLogger?.Information(message);
            _fileLogger    ?.Information(message);
        }

        public static void Info(object sender, string name, Dictionary<string, string> eventTrack = null)
        {
            string message = LogUtil.log(sender, name, eventTrack);

            _fileLogger   ?.Information(message);
            _consoleLogger?.EventToConsole(LogEventLevel.Information, LogUtil.log(sender), name, eventTrack);

            //LogMaster.TelemetryClient?.TrackEvent(name, eventTrack);
        }

        public static void Warn(object sender, string message, params object[] args)
        {
            message = LogUtil.log(sender, message);

            _consoleLogger ?.TextToConsole(LogEventLevel.Warning, message, args);
            _insightsLogger?.Warning(message);
            _fileLogger    ?.Warning(message);
        }

        public static void Error(object sender, Exception ex, string context = null, List<string> logs = null)
        {
            string message = LogUtil.err(sender, ex, context);

            _consoleLogger ?.TextToConsole(LogEventLevel.Error, message);
            _insightsLogger?.Error(message);
            _fileLogger    ?.Error(message);

            //LogMaster.TelemetryClient?.TrackException(ex);
            //steps?.Next(LogUtil.log(sender, $"ERROR: {ex.Short()}"));
        }

        public static void Block(object sender, IEnumerable<string> messages, string header = null, bool isDebug = false)
        {
            if (messages.IsEmpty()) return;
            
            string message = LogUtil.block(sender, messages, header);

            if (isDebug)
            {
                _consoleLogger?.TextToConsole(LogEventLevel.Debug, message);
                _fileLogger?.Debug(message);
            }
            else
            {
                _consoleLogger?.TextToConsole(LogEventLevel.Information, message);
                _fileLogger?.Information(message);
            }
        }
    }

    public static class CustomConsoleThemes       // Core.Lib.Util.Telemetry.Seri.CustomConsoleThemes
    {
        public static SystemConsoleTheme Pro => new SystemConsoleTheme(
                new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle>
                {
                    [ConsoleThemeStyle.Text] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Gray },
                    [ConsoleThemeStyle.SecondaryText] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
                    [ConsoleThemeStyle.TertiaryText] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
                    [ConsoleThemeStyle.Invalid] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Yellow },
                    [ConsoleThemeStyle.Null] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White },
                    [ConsoleThemeStyle.Name] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White },
                    [ConsoleThemeStyle.String] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkYellow },
                    [ConsoleThemeStyle.Number] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkCyan },
                    [ConsoleThemeStyle.Boolean] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White },
                    [ConsoleThemeStyle.Scalar] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White },
                    [ConsoleThemeStyle.LevelVerbose] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray, Background = ConsoleColor.Black },
                    [ConsoleThemeStyle.LevelDebug] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray, Background = ConsoleColor.Black },
                    [ConsoleThemeStyle.LevelInformation] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Gray, Background = ConsoleColor.Black },
                    [ConsoleThemeStyle.LevelWarning] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Yellow, Background = ConsoleColor.Black },
                    [ConsoleThemeStyle.LevelError] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Red, Background = ConsoleColor.Black },
                    [ConsoleThemeStyle.LevelFatal] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
                });
    }
}
