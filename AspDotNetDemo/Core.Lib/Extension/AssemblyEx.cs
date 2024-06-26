﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Clio.Demo.Core.Lib.Extension
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
        public static string RunTime(this Assembly asm)
        {
            string runtime = "?";
            if (null != asm)
            {
                AssemblyName netName = asm.GetReferencedAssemblies().FirstOrDefault(x => x.Name.ToLower() == "system.runtime");
                AssemblyName stdName = asm.GetReferencedAssemblies().FirstOrDefault(x => x.Name.ToLower() == "netstandard");

                if (null != netName) runtime = $".net {netName.Version.Major}.{netName.Version.Minor}";
                if (null != stdName) runtime = $" std {stdName.Version.Major}.{stdName.Version.Minor}";
            }
            return runtime;
        }

        public static string Info(this Assembly asm, bool where = false)
		{
			if (null == asm) return string.Empty;

            return $"{asm.GetAttribute<AssemblyTitleAttribute>(x => x.Title)}".PadRight(30) +
                   $"{asm.RunTime()}   " +
                   $"ver: {asm.Version()} '{asm.BuildTime()}' {(where ? $" {asm.ManifestModule.FullyQualifiedName}" : string.Empty)}" +
                   $"{asm.GetAttribute<AssemblyDescriptionAttribute>(x => x.Description)}";
        }

        private static string GetAttribute<T>(this Assembly asm, Func<T, string> getElement, string none = "n/a") where T : Attribute
		{
			object[] values = asm.GetCustomAttributes(typeof(T), false);
			T attribute = values.Length > 0 ? values[0] as T : null;

			return null == attribute ? none : getElement(attribute);
		}
	}
}
