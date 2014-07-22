using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Jint;

namespace Duster.Net
{
	public class DustCompiler
	{
		private readonly static Lazy<string> DustJs = new Lazy<string>(() =>
		{
			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Duster.Net.dust-full-0.3.0.min.js"))
			{
				using (var reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		});

		/// <summary>
		/// Compiles dust template into JavaScript
		/// </summary>
		/// <param name="name">Template Name</param>
		/// <param name="markup">Template Markup</param>
		/// <returns>Compiled JavaScript</returns>
		public static string Compile(string name, string markup)
		{
			return new Engine()
				.Execute(string.Format("{0}dust.compile('{1}','{2}')", DustJs.Value, Sanitize(markup), name))
				.GetCompletionValue()
				.ToString();
		}


		private static string Sanitize(string content)
		{
			var newLineStripped = Regex.Replace(content, @"[\n\t\r]", "", RegexOptions.Compiled);
			var stripWhitespaceSafe = Regex.Replace(newLineStripped, @">\s+<", "><", RegexOptions.Compiled);

			return stripWhitespaceSafe;
		}
	}
}
