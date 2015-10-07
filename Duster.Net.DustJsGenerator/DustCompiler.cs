using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Jint;

namespace Duster.Net
{
    public class DustCompiler
    {
        private static readonly Lazy<string> DustJs = new Lazy<string>(ValueFactory);

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        private static string ValueFactory()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Duster.Net.dust-full-2.7.2.min.js"))
            {
                if (stream == null)
                    throw new Exception("Couldn't get manifest resource stream for dust-full-2.7.2.min.js");

                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Compiles dust template into JavaScript
        /// </summary>
        /// <param name="name">Template Name</param>
        /// <param name="markup">Template Markup</param>
        /// <returns>Compiled JavaScript</returns>
        public static string Compile(string name, string markup)
        {
            return new Engine()
                .Execute($"{DustJs.Value}dust.compile('{Sanitize(markup)}','{name}')")
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
