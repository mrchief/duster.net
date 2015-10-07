using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSLangProj80;

namespace Duster.Net
{
    [ComVisible(true)]
	[Guid(GuidList.GuidPkgString)]
	[CodeGeneratorRegistration(typeof(DustJsGenerator), "Dust JS Compiled Template Generator", vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
	[CodeGeneratorRegistration(typeof(DustJsGenerator), "Dust JS Compiled Template Generator", vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
	[CodeGeneratorRegistration(typeof(DustJsGenerator), "Dust JS Compiled Template Generator", vsContextGuids.vsContextGuidVJSProject, GeneratesDesignTimeSource = true)]
	[ProvideObject(typeof(DustJsGenerator), RegisterUsing = RegistrationMethod.CodeBase)]
	public class DustJsGenerator : ObjectWithSite, IVsSingleFileGenerator
    {
		#region IVsSingleFileGenerator Members

		public int DefaultExtension(out string pbstrDefaultExtension)
		{
			pbstrDefaultExtension = ".dust.js";
			return VSConstants.S_OK;
		}

		public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
		{
			var generatedText = DustCompiler.Compile(Path.GetFileNameWithoutExtension(wszInputFilePath), bstrInputFileContents);
			var bytes = Encoding.UTF8.GetBytes($"/*! Generated with dustjs-linkedin - v2.7.2 */{Environment.NewLine}{generatedText}");

            rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(bytes.Length);
			Marshal.Copy(bytes, 0, rgbOutputFileContents[0], bytes.Length);
			pcbOutput = (uint)bytes.Length;

			return VSConstants.S_OK;
		}

		#endregion
	}
}
