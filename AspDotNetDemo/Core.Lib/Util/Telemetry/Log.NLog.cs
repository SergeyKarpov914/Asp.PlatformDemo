﻿using Clio.Demo.Core.Util;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;

namespace Clio.Demo.Util.Telemetry.NLog
{
    public static class Log
    {
        private static Logger _logger;

        static Log()
        {
            LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
            _logger = LogManager.GetCurrentClassLogger();
        }

        public static void Info(object sender, string message)
        {
            _logger.Info(LogUtil.log(sender, message));
        }
        public static void Error(object sender, Exception ex, List<string> logs = null)
        {
            _logger.Error(LogUtil.err(sender, ex));
            logs?.Add(LogUtil.log(sender, $"ERROR: {ex.Message}"));
        }
        public static void Block(object sender, IEnumerable<string> messages, string header = null)
        {
            _logger.Info(LogUtil.block(sender, messages, header));
        }
        public static void BlockDbg(object sender, IEnumerable<string> messages, string header = null)
        {
            _logger.Debug(LogUtil.block(sender, messages, header));
        }
    }
}