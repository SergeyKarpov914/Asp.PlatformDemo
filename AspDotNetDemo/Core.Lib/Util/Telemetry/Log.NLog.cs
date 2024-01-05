using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;

namespace Clio.Demo.Core.Lib.Util
{
    public static class Nlog
    {
        private static Logger _logger;

        static Nlog()
        {
            LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
            _logger = LogManager.GetCurrentClassLogger();
        }

        public static void Debug(object sender, string message)
        {
            _logger.Debug(LogUtil.log(sender, message));
        }
        public static void Info(object sender, string message)
        {
            _logger.Info(LogUtil.log(sender, message));
        }
        public static void Warn(object sender, string message)
        {
            _logger.Warn(LogUtil.log(sender, message));
        }
        public static void Error(object sender, Exception ex, List<string> logs = null)
        {
            _logger.Error(LogUtil.err(sender, ex));
            logs?.Add(LogUtil.log(sender, $"ERROR: {ex.Message}"));
        }
        public static void Block(object sender, IEnumerable<string> messages, string header = null, bool isDebug = false)
        {
            if (isDebug)
            {
                _logger.Debug(LogUtil.block(sender, messages, header));
            }
            else
            {
                _logger.Info(LogUtil.block(sender, messages, header));
            }
        }
    }
}
