using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.Seri;
using Core.Lib.Extension;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Clio.Demo.Core.Util
{
    public class LogMaster : ILog
    {
        public IConfiguration Configuration { get; private set; }
        public TelemetryClient TelemetryClient { get; private set; }

        public LogMaster(IConfiguration configuration, TelemetryClient telemetryClient)
        {
            Configuration = configuration;
            TelemetryClient = telemetryClient;
        }
        public LogMaster(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }

    public static class LogUtil
    {
        #region log formatting

        public static string log(object sender)
        {
            return $"{LogThread} {sender.Stamp()}\t";
        }

        public static string log(object sender, string message)
        {
            return $"{LogThread} {sender.Stamp()}\t{message}";
        }

        public static string log(object sender, string name, Dictionary<string, string> eventTrack)
        {
            return $"{LogThread} {sender.Stamp()}\t'{name}' {string.Join(", ", eventTrack.Keys.Select(x => $"{x}:{eventTrack[x]}"))}";
        }

        public static string err(object sender, Exception ex, string context = null)
        {
            return $"{LogThread} {sender.Stamp()} {context}\t{ex.Short()}";
        }

        public static string block(object sender, IEnumerable<string> messages, string header = null)
        {
            if (null == messages || null == sender || !messages.Any(x => !x.IsEmpty()))
            {
                return string.Empty;
            }
            string prefix = $"{LogThread} {sender.Stamp()}  {header}";
            string newLine = "\r\n\t";

            StringBuilder sb = new StringBuilder();

            sb.Append($"{prefix}{newLine}--------------------------------------------------------------------------------------------");
            foreach (string message in messages)
            {
                sb.Append($"{newLine}{message}");
            }
            sb.Append($"{newLine}--------------------------------------------------------------------------------------------");
            return sb.ToString();
        }

        #endregion log formatting

        public static string  Exe       => Process.GetCurrentProcess().ProcessName;
        public static decimal Mem       => Process.GetCurrentProcess().WorkingSet64 / 1024;
        public static string  ThreadId  => Thread.CurrentThread.Info();
        public static string  TimeStamp => DT.TimeLong;
        public static string  WhoAmI    => $"{Environment.MachineName}:{Environment.UserName}";
        public static string  Snap      => $"{TimeStamp} {LogThread}";
        public static string  LogMemory => $"{Exe}: {Mem:N0} kb".PadLeft(18);
        public static string  LogThread => $"[{ThreadId}]".PadLeft(7);

        public static string Caller(int up = 2)
        {
            StackTrace stack = new StackTrace();

            MethodBase method = new StackTrace().GetFrame(up).GetMethod();
            Type methodType   = method.ReflectedType;

            return $"{(methodType is not null ? methodType.DisplayName() : "?")}.{method.Name}";
        }

        public static void RuntimeInfo(object sender, string context, string[] args = null, Type[] subTypes = null)
        {
            List<string> messages = new List<string>();
            try
            {
                Process process = Process.GetCurrentProcess();
                AppDomain domain = Thread.GetDomain();

                messages.AddRange(new[]
                {
                    $"{sender.GetType().Assembly.Info()}",
                    $"{WhoAmI} PID {process.Id} AppDomain {domain.FriendlyName} ({(domain.IsDefaultAppDomain() ? "Default" : "Custom")})",
                    $"{process.MainModule.FileName}",
                });
                if (null != args)
                {
                    messages.Add($"Args: {string.Join(" ", args)}");
                }
                if (null != subTypes)
                {
                    messages.Add("Assemblies:");

                    foreach (Type subType in subTypes)
                    {
                        messages.Add($"   {subType.Assembly.Info()}");
                    }
                }
            }
            catch
            {
            }
            Log.Block(sender, messages, $"{context}\t{DateTime.Now.ToShortDateString()}");
        }

        public static void ExitInfo(object sender, int exitCode, string context = null)
        {
            Process process = Process.GetCurrentProcess();
            Log.Block(sender, new[] { $"Exit code {exitCode}", $"{process.MainModule.FileName}" }, $"Exit {context}");
        }

        public static IEnumerable<string> ConfigurationContent(object sender, IConfigurationRoot configuration, bool log = true)
        {
            List<string> all = new List<string>();
            try
            {
                string allConfig = configuration.GetDebugView();
                string[] cfgLines = allConfig.Split('\n');

                string sectionKey = null;

                List<string> flat = new List<string>();

                foreach (string line in cfgLines) //providerLines)
                {
                    string logLine = line.Replace('\r', '\0');

                    if (logLine.EndsWith(":"))  // section name
                    {
                        if (null == sectionKey)
                        {
                            sectionKey = logLine;
                        }
                        else
                        {
                            sectionKey += logLine;
                        }
                    }
                    if (logLine.StartsWith(" ") && logLine.EndsWith(")")) // section body
                    {
                        if (null != sectionKey)
                        {
                            all.Add(sectionKey);
                            sectionKey = null;
                        }
                        all.Add(logLine.NoLongerThan(150));
                    }
                    if (!logLine.Contains("EnvironmentVariablesConfigurationProvider") && !logLine.StartsWith(" ") && logLine.EndsWith(")"))
                    {
                        flat.Add(logLine);
                    }
                }
                foreach (string line in flat)  // avoid showing unrelated configuration (for now)
                {
                    all.Add(line.NoLongerThan(150));
                }
            }
            catch
            {
            }
            finally
            {
                if (log && !all.IsEmpty())
                {
                    Log.BlockDbg(sender, all, "Configuration");
                }
            }
            return all;
        }

        private static string hidePwd(string line)
        {
            string outLine = line.Replace('\n', ' ').Replace('\r', ' ');

            if (line.Trim().ToLower().StartsWith("dbpath="))
            {
                outLine = line.Replace("dbpath=", "");
            }
            string[] segments = line.Split(';');

            foreach (string segment in segments)
            {
                if (segment.Trim().ToLower().StartsWith("pwd"))
                {
                    outLine = line.Replace(segment.Trim(), "Pwd=<...>");
                }
            }
            return outLine;
        }
    }

    public sealed class Steps : List<string>
    {
		private readonly object _owner;
        private readonly string _context;
        private readonly Stopwatch _timing;

		public bool HasError { get; private set; }

		private Steps(object owner, string context)
        {
            _owner = owner;
            _context = context;
            _timing = new Stopwatch();
            _timing.Start();
        }

        public static Steps Start(object owner, string message = null, string context = null, int upStack = 2)
        {
            string caller = LogUtil.Caller(upStack);
            return new Steps(owner, $"{(context ?? caller)}").Next(message);
        }

        public static Steps Start(object owner, string message)
        {
            return Steps.Start(owner, message, null, 5);
        }

        public Steps Next(string message)
        {
            if (message is not null)
            {
                Add($"{message}  ({LogUtil.Snap})");
            }
            return this;
        }

        public Steps Error(Exception ex)
        {
            if (ex is not null)
            {
                Add($"{LogUtil.Snap} ERROR! {ex.Message}");
				HasError = true;
			}
            return this;
        }

        public Steps Next(IEnumerable<string> messages)
        {
            if (!messages.IsEmpty())
            {
                AddRange(messages.Select(x => $"{x}  ({LogUtil.Snap})"));
            }
            return this;
        }

        public void Stop(string message = null)
        {
            _timing.Stop();

            string time = $"({_timing.Elapsed.TotalMilliseconds:N3} ms)";

            if (null == message && Count == 1)
            {
                Log.Info(_owner, $"{time.PadLeft(14)} {_context} {this[0]}");
            }
            else
            {
                Log.Block(_owner, Next(message), $"{time.PadLeft(14)} {_context}");
            }
        }
    }
}


