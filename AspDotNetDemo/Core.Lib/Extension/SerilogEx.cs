using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clio.Demo.Core.Lib.Extension
{
    public static class SeriLoggerEx
    {
        private static Dictionary<LogEventLevel, Tuple<string, string>> Colors =
            new Dictionary<LogEventLevel, Tuple<string, string>>
            {
                [LogEventLevel.Verbose]     = new Tuple<string, string>("\u001b[37m", "\u001b[90m"),
                [LogEventLevel.Debug]       = new Tuple<string, string>("\u001b[90m", "\u001b[90m"),
                [LogEventLevel.Information] = new Tuple<string, string>("\u001b[37m", "\u001b[97m"),
                [LogEventLevel.Warning]     = new Tuple<string, string>("\u001b[33m", "\u001b[93m"),
                [LogEventLevel.Error]       = new Tuple<string, string>("\u001b[31m", "\u001b[91m"),
                [LogEventLevel.Fatal]       = new Tuple<string, string>("\u001b[91m", "\u001b[91m"),
            };

        public static void TextToConsole(this ILogger logger, LogEventLevel level, string messageTemplate, params object[] args)
        {
            string textColor = Colors[level].Item1;
            string argColor = Colors[level].Item2;

            string propName = "IsImportant";
            string argOpen = $"{argColor}{"{"}";
            string argClose = $"{"}"}{textColor}";
            string coloredTemplate = $"{textColor}{messageTemplate.Replace("{", argOpen).Replace("}", argClose)}";

            switch (level)
            {
                case LogEventLevel.Information:
                    logger.ForContext(propName, true).Information(coloredTemplate, args);
                    break;
                case LogEventLevel.Debug:
                    logger.ForContext(propName, true).Debug(coloredTemplate, args);
                    break;
                case LogEventLevel.Warning:
                    logger.ForContext(propName, true).Warning(coloredTemplate, args);
                    break;
                case LogEventLevel.Error:
                    logger.ForContext(propName, true).Error(coloredTemplate, args);
                    break;
            }
        }

        public static void EventToConsole(this ILogger logger, LogEventLevel level, string prefix, string name, Dictionary<string, string> eventTrack)
        {
            string textColor = ForegroundBlue;
            string argColor = ForegroundStrongBlue;

            string propName = "IsImportant";
            string coloredTemplate = $"{prefix}{textColor}'{name}' {string.Join(", ", eventTrack.Keys.Select(x => $"{x}:{argColor}{eventTrack[x].Last()}{textColor}"))}";

            switch (level)
            {
                case LogEventLevel.Information:
                    logger.ForContext(propName, true).Information(coloredTemplate);
                    break;
                case LogEventLevel.Error:
                    logger.ForContext(propName, true).Error(coloredTemplate);
                    break;
            }
        }

        #region colors

        public const string ForegroundBlack   = "\u001b[30m";
        public const string ForegroundRed     = "\u001b[31m";
        public const string ForegroundGreen   = "\u001b[32m";
        public const string ForegroundYellow  = "\u001b[33m";
        public const string ForegroundBlue    = "\u001b[34m";
        public const string ForegroundMagenta = "\u001b[35m";
        public const string ForegroundCyan    = "\u001b[36m";
        public const string ForegroundWhite   = "\u001b[37m";

        public const string ForegroundGray    = "\u001b[90m"; // strong black

        public const string ForegroundStrongRed     = "\u001b[91m";
        public const string ForegroundStrongGreen   = "\u001b[92m";
        public const string ForegroundStrongYellow  = "\u001b[93m";
        public const string ForegroundStrongBlue    = "\u001b[94m";
        public const string ForegroundStrongMagenta = "\u001b[95m";
        public const string ForegroundStrongCyan    = "\u001b[96m";
        public const string ForegroundStrongWhite   = "\u001b[97m";


        public const string BackgroundBlack   = "\u001b[40m";
        public const string BackgroundRed     = "\u001b[41m";
        public const string BackgroundGreen   = "\u001b[42m";
        public const string BackgroundYellow  = "\u001b[43m";
        public const string BackgroundBlue    = "\u001b[44m";
        public const string BackgroundMagenta = "\u001b[45m";
        public const string BackgroundCyan    = "\u001b[46m";
        public const string BackgroundWhite   = "\u001b[47m";
        public const string BackgroundBrightBlack   = "\u001b[40;1m";
        public const string BackgroundBrightRed     = "\u001b[41;1m";
        public const string BackgroundBrightGreen   = "\u001b[42;1m";
        public const string BackgroundBrightYellow  = "\u001b[43;1m";
        public const string BackgroundBrightBlue    = "\u001b[44;1m";
        public const string BackgroundBrightMagenta = "\u001b[45;1m";
        public const string BackgroundBrightCyan    = "\u001b[46;1m";
        public const string BackgroundBrightWhite   = "\u001b[47;1m";

        #endregion colors
    }
}
