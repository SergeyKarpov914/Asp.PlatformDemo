using System;
using System.IO;
using System.Reflection;

namespace Clio.Demo.Extension
{
	public static class AssemblyEx
	{
		public static string Version(this Assembly asm)
		{
			if (null == asm) return string.Empty;
			return $"{asm.GetName().Version.ToString()}";
		}
		public static string Name(this Assembly asm)
		{
			if (null == asm) return string.Empty;
			return asm.GetName().Name;
		}
		public static string BuildTime(this Assembly asm)
		{
			if (null == asm) return string.Empty;
			return File.GetLastWriteTime(asm.Location).ToString("MMM dd yyyy HH:mm");
		}
		public static string Info(this Assembly asm, bool where = false)
		{
			if (null == asm) return string.Empty;

			return $"{asm.GetAttribute<AssemblyTitleAttribute>(x => x.Title)} " +
	     		   $"[{asm.GetAttribute<AssemblyDescriptionAttribute>(x => x.Description)}] " +
			       $"ver: {asm.Version()} '{asm.BuildTime()}' {(where ? $" {asm.ManifestModule.FullyQualifiedName}" : string.Empty)}";
		}
		
		private static string GetAttribute<T>(this Assembly asm, Func<T, string> getElement, string none = "n/a") where T : Attribute
		{
			object[] values = asm.GetCustomAttributes(typeof(T), false);
			T attribute = values.Length > 0 ? values[0] as T : null;

			return null == attribute ? none : getElement(attribute);
		}
	}
}
