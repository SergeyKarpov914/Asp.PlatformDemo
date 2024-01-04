using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clio.Demo.Extension
{
	public static class HtmlBuilderEx
	{
		public static void Style(this StringBuilder sb, string data)
		{
			if (data.IsEmpty()) return;
			sb.Append($"<style type=\"text/css\">\n\r{data}\n\r</style>");
		}
		public static void Header(this StringBuilder sb, string data)
		{
			if (data.IsEmpty()) return;
			sb.Append($"<font face=Arial color=MAROON size=3>{data}</font><br><br/><br/>");
		}
		public static void Body(this StringBuilder sb, string data)
		{
			if (data.IsEmpty()) return;
			sb.Append($"<br/><font face=Arial color=DARKSLATEBLUE size=2>{data}</font><br/>");
		}
		public static void Link(this StringBuilder sb, string data)
		{
			if (data.IsEmpty()) return;
			string[] parts = data.Split(new char[] { '|' });
			if (null == parts || parts.Length < 2)
			{
				return;
			}
			sb.Append($"<br/><a href=\"file:///{parts[0]}\">{parts[1]}</a><br/>");
		}
		public static void Context(this StringBuilder sb, string data)
		{
			if (data.IsEmpty()) return;

			IEnumerable<string> lines = toFramed(data);
			foreach (string line in lines)
			{
				sb.Append(line);
			}
		}
		public static void Stamp(this StringBuilder sb, string data)
		{
			if (data.IsEmpty()) return;
			sb.Append($"<br><font face=Arial color=GAINSBORO size=2>{data}</font></br>");
		}

		public static void Table(this StringBuilder sb, IEnumerable<Dictionary<string, string>> table)
		{
			if (table.IsEmpty()) return;

			IEnumerable<string> columns = table.FirstOrDefault().Keys;

			sb.Append("<table class='gridtable'>");
			sb.Append("<ts>");

			foreach (string column in columns)
			{
				sb.Append($"<th>{column}</th>");
			}
			sb.Append("</ts>");

			foreach (Dictionary<string, string> row in table)
			{
				sb.Append("<tr>");

				foreach (string column in columns)
				{
					sb.Append($"<td align=\"right\">{row[column]}</td>");
				}
				sb.Append("</tr>");
			}
			sb.Append("</table>");
		}

		private static string[] toFramed(string text)
		{
			string[] lines = text.Split(new char[] { '|' });
			List<string> framed = new List<string> { "<table class='gridtable'>" };

			framed.AddRange(lines.Where(x => !x.IsEmpty()).Select(x => $"<tr><td>{x}</td></tr>"));
			framed.Add("</table><br/>");

			return framed.ToArray();
		}
	}
}
