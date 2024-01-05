using System;
using System.Threading;

namespace Clio.Demo.Core.Lib.Extension
{
    public static class ThreadEx
	{
		public static string Info(this Thread thread)
		{
			return null != thread ? $"{thread.ManagedThreadId}{(thread.IsBackground ? "B" : "F")}{(thread.IsThreadPoolThread ? "P" : "D")}" : "";
		}

        public static Thread OnFrontThread(ThreadStart proc, Action<string> log = null)
        {
            Thread thread = new Thread(proc);
            thread.IsBackground = false;
#if Windows
            thread.SetApartmentState(ApartmentState.STA);
#endif
            log?.Invoke(thread.Info());
            thread.Start();
            return thread;
        }
    }
}
